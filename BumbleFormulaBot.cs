using Microsoft.ML;
using Microsoft.ML.Data;
using System.Diagnostics;

namespace BumbleFormula
{
    public static class BumbleFormulaBot
    {
        private static readonly string modelPath = Path.Combine(AppContext.BaseDirectory, "model.zip");
        private static readonly MLContext mlContext = new MLContext();

        public static string Predict(Dictionary<string, double> features)
        {
            Debug.WriteLine($"Starting prediction. Model path: {modelPath}");

            if (!File.Exists(modelPath))
            {
                Debug.WriteLine("Model file not found. Starting model training...");
                TrainingModel.TrainModel();
                Debug.WriteLine("Model training completed. Loading the trained model...");
            }
            else
            {
                Debug.WriteLine("Model file found. Proceeding to load the model...");
            }

            // Load trained model
            ITransformer trainedModel = mlContext.Model.Load(modelPath, out _);
            Debug.WriteLine("Model loaded successfully.");

            // Create PredictionEngine
            Debug.WriteLine("Creating prediction engine...");
            var predEngine = mlContext.Model.CreatePredictionEngine<BumbleData, BumblePrediction>(trainedModel);
            Debug.WriteLine("Prediction engine created.");

            // Create input data object
            BumbleData inputData = new BumbleData
            {
                age = (float)features["age"],
                numberofherpictures = (float)features["numberofherpictures"],
                numberoflinesinbio = (float)features["numberoflinesinbio"],
                height = (float)features["height"],
                physicalactivity = (float)features["physicalactivity"],
                education = (float)features["education"],
                drinking = (float)features["drinking"],
                smoking = (float)features["smoking"],
                wantchildren = (float)features["wantchildren"],
                havekids = (float)features["havekids"],
                politics = (float)features["politics"],
                nils = (float)features["nils"]
            };

            Debug.WriteLine($"Input Data: Age={inputData.age}, NumberofHerPictures={inputData.numberofherpictures}, NumberofLinesInBio={inputData.numberoflinesinbio}, Height={inputData.height}");

            // Make predictions
            var prediction = predEngine.Predict(inputData);
            Debug.WriteLine("Prediction made.");

            // Class labels
            string[] classLabels = new[] { "gamer",
"later",
"handsitter",
"seemserious",
"shamelessgold",
"unfriendly",
"fwb",
"friend",
"parasite",
"serious",
"paranoid",
"claimserious",
"shamefulgold",
"business",
"hk", };

            // Get predictions with scores over 10%
            var predictionsAbove60 = prediction.Scores
                .Select((score, index) => new { Label = classLabels[index], Score = score })
                .Where(x => x.Score >= 0.1)
                .OrderByDescending(x => x.Score)
                .Select(x => $"{x.Label}: {x.Score * 100}%")
                .ToArray();

            Debug.WriteLine("Predictions filtered. Scores over 10");
            foreach (var pred in predictionsAbove60)
            {
                Debug.WriteLine(pred);
            }

            return string.Join("\n", predictionsAbove60);
        }
    }

    public class BumbleData
    {
        public float age { get; set; }
        public float numberofherpictures { get; set; }
        public float numberoflinesinbio { get; set; }
        public float height { get; set; }
        public float physicalactivity { get; set; }
        public float education { get; set; }
        public float drinking { get; set; }
        public float smoking { get; set; }
        public float wantchildren { get; set; }
        public float havekids { get; set; }
        public float politics { get; set; }
        public float nils { get; set; }

        // Include category as the label for training, but it will be ignored during inference
        [ColumnName("category"), LoadColumn(12)]
        public string category { get; set; }
    }

    public class BumblePrediction
    {
        public string PredictedLabel { get; set; }

        [ColumnName("Score")]
        public float[] Scores { get; set; }
    }
}
