using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers.FastTree;
using System;
using System.Diagnostics;
using System.IO;


namespace BumbleFormula
{
    public class TrainingModel
    {
        private static readonly string dataPath = Path.Combine(AppContext.BaseDirectory, "BumbleDataADASYN.csv");
        private static readonly string modelPath = Path.Combine(AppContext.BaseDirectory, "model2.zip");
        private static readonly string modelOnnxPath = Path.Combine(AppContext.BaseDirectory, "model2.onnx");
        private static readonly MLContext mlContext = new MLContext();

        public static void TrainModel()
        {
            Debug.WriteLine($"Starting training model. Data path: {dataPath}, Model path: {modelPath}, Model ONNX path: {modelOnnxPath}");

            // Load data
            Debug.WriteLine("Loading data...");
            var textLoader = mlContext.Data.CreateTextLoader(new TextLoader.Options
            {
                Columns = new[]
                {
                    new TextLoader.Column("age", DataKind.Single, 0),
                    new TextLoader.Column("numberofherpictures", DataKind.Single, 1),
                    new TextLoader.Column("numberoflinesinbio", DataKind.Single, 2),
                    new TextLoader.Column("height", DataKind.Single, 3),
                    new TextLoader.Column("physicalactivity", DataKind.Single, 4),
                    new TextLoader.Column("education", DataKind.Single, 5),
                    new TextLoader.Column("drinking", DataKind.Single, 6),
                    new TextLoader.Column("smoking", DataKind.Single, 7),
                    new TextLoader.Column("wantchildren", DataKind.Single, 8),
                    new TextLoader.Column("havekids", DataKind.Single, 9),
                    new TextLoader.Column("politics", DataKind.Single, 10),
                    new TextLoader.Column("nils", DataKind.Single, 11),
                    new TextLoader.Column("profilenils", DataKind.Single, 12),
                    new TextLoader.Column("bionils", DataKind.Single, 13),
                    new TextLoader.Column("lookingnils", DataKind.Single, 14),
                    new TextLoader.Column("causenils", DataKind.Single, 15),
                    new TextLoader.Column("interestnils", DataKind.Single, 16),
                    new TextLoader.Column("category", DataKind.String, 17)
                },
                HasHeader = true,
                Separators = new[] { ',' }
            });

            IDataView dataView = textLoader.Load(dataPath);
            Debug.WriteLine($"Data loaded. Number of rows: {dataView.GetRowCount()}");

            // Define features pipeline with missing value replacement and multiclass classification
            Debug.WriteLine("Defining pipeline...");
            var pipeline = mlContext.Transforms.ReplaceMissingValues(new[] { new InputOutputColumnPair(@"age", @"age"), new InputOutputColumnPair(@"numberofherpictures", @"numberofherpictures"), new InputOutputColumnPair(@"numberoflinesinbio", @"numberoflinesinbio"), new InputOutputColumnPair(@"height", @"height"), new InputOutputColumnPair(@"physicalactivity", @"physicalactivity"), new InputOutputColumnPair(@"education", @"education"), new InputOutputColumnPair(@"drinking", @"drinking"), new InputOutputColumnPair(@"smoking", @"smoking"), new InputOutputColumnPair(@"wantchildren", @"wantchildren"), new InputOutputColumnPair(@"havekids", @"havekids"), new InputOutputColumnPair(@"politics", @"politics"), new InputOutputColumnPair(@"nils", @"nils"), new InputOutputColumnPair(@"profilenils", @"profilenils"), new InputOutputColumnPair(@"bionils", @"bionils"), new InputOutputColumnPair(@"lookingnils", @"lookingnils"), new InputOutputColumnPair(@"causenils", @"causenils"), new InputOutputColumnPair(@"interestnils", @"interestnils") })
                                    .Append(mlContext.Transforms.Concatenate(@"Features", new[] { @"age", @"numberofherpictures", @"numberoflinesinbio", @"height", @"physicalactivity", @"education", @"drinking", @"smoking", @"wantchildren", @"havekids", @"politics", @"nils", @"profilenils", @"bionils", @"lookingnils", @"causenils", @"interestnils" }))
                                    .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: @"category", inputColumnName: @"category", addKeyValueAnnotationsAsText: false))
                                    .Append(mlContext.MulticlassClassification.Trainers.OneVersusAll(binaryEstimator: mlContext.BinaryClassification.Trainers.FastTree(new FastTreeBinaryTrainer.Options() { 
                                        NumberOfLeaves = 14, 
                                        MinimumExampleCountPerLeaf = 42, 
                                        NumberOfTrees = 62, 
                                        MaximumBinCountPerFeature = 338, 
                                        FeatureFraction = 0.9110511012223066, 
                                        LearningRate = 0.9999997766729865, 
                                        LabelColumnName = @"category", 
                                        FeatureColumnName = @"Features", 
                                        DiskTranspose = false }), 
                                        labelColumnName: @"category"))
                                    .Append(mlContext.Transforms.Conversion.MapKeyToValue(outputColumnName: @"PredictedLabel", inputColumnName: @"PredictedLabel"));


            Debug.WriteLine("Pipeline defined. Starting training...");

            // Train model
            var model = pipeline.Fit(dataView);

            Debug.WriteLine("Model training complete.");

            // Save model in ZIP format
            Debug.WriteLine("Saving model in ZIP format...");
            mlContext.Model.Save(model, dataView.Schema, modelPath);
            Debug.WriteLine($"Model saved to {modelPath}");

            // Convert model to ONNX format
            Debug.WriteLine("Saving model in ONNX format...");
            using (var stream = new FileStream(modelOnnxPath, FileMode.Create, FileAccess.Write))
            {
                // Convert to ONNX
                mlContext.Model.ConvertToOnnx(model, dataView, stream); // Ensure the method signature matches
            }
            Debug.WriteLine($"Model saved to {modelOnnxPath}");
        }
    }
}
