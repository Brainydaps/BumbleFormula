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
        private static readonly string dataPath = Path.Combine(AppContext.BaseDirectory, "bumbleDataFinal.csv");
        private static readonly string modelPath = Path.Combine(AppContext.BaseDirectory, "model.zip");
        private static readonly MLContext mlContext = new MLContext();

        public static void TrainModel()
        {
            Debug.WriteLine($"Starting training model. Data path: {dataPath}, Model path: {modelPath}");

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
                    new TextLoader.Column("category", DataKind.String, 12)
                },
                HasHeader = true,
                Separators = new[] { ',' }  // Use Separators instead of SeparatorChar
            });

            IDataView dataView = textLoader.Load(dataPath);
            Debug.WriteLine($"Data loaded. Number of rows: {dataView.GetRowCount()}");

            // Define features pipeline with missing value replacement and multiclass classification
            Debug.WriteLine("Defining pipeline...");
            var pipeline = mlContext.Transforms.ReplaceMissingValues(new[]
                        {
                            new InputOutputColumnPair("age", "age"),
                            new InputOutputColumnPair("numberofherpictures", "numberofherpictures"),
                            new InputOutputColumnPair("numberoflinesinbio", "numberoflinesinbio"),
                            new InputOutputColumnPair("height", "height"),
                            new InputOutputColumnPair("physicalactivity", "physicalactivity"),
                            new InputOutputColumnPair("education", "education"),
                            new InputOutputColumnPair("drinking", "drinking"),
                            new InputOutputColumnPair("smoking", "smoking"),
                            new InputOutputColumnPair("wantchildren", "wantchildren"),
                            new InputOutputColumnPair("havekids", "havekids"),
                            new InputOutputColumnPair("politics", "politics"),
                            new InputOutputColumnPair("nils", "nils")
                        })
                        .Append(mlContext.Transforms.Concatenate("Features", new[]
                        {
                            "age", "numberofherpictures", "numberoflinesinbio", "height", "physicalactivity", "education", "drinking", "smoking", "wantchildren", "havekids", "politics", "nils"
                        }))
                        .Append(mlContext.Transforms.Conversion.MapValueToKey("category", "category"))
                        .Append(mlContext.MulticlassClassification.Trainers.OneVersusAll(
                            mlContext.BinaryClassification.Trainers.FastTree(new FastTreeBinaryTrainer.Options
                            {
                                NumberOfLeaves = 9,
                                MinimumExampleCountPerLeaf = 17,
                                NumberOfTrees = 173,
                                MaximumBinCountPerFeature = 59,
                                FeatureFraction = 0.99999999,
                                LearningRate = 0.5422530690393761,
                                LabelColumnName = "category",
                                FeatureColumnName = "Features"
                            }),
                            labelColumnName: "category"))
                        .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            Debug.WriteLine("Pipeline defined. Starting training...");

            // Train model
            var model = pipeline.Fit(dataView);

            Debug.WriteLine("Model training complete.");

            // Save model
            Debug.WriteLine("Saving model...");
            mlContext.Model.Save(model, dataView.Schema, modelPath);
            Debug.WriteLine($"Model saved to {modelPath}");
        }
    }
}
