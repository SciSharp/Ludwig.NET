using Ludwig.Core;
using Ludwig.Core.AppModels;
using System;

namespace Ludwig.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            // var ludwig = new LudwigModel("model_definition.yaml");
            // ludwig.Train(data_csv: "text_classification.csv");
            var ludwig = LudwigModel.Load("results/api_experiment_run_4/model");
            ludwig.Predict("text_classification_predict.csv");

            Console.ReadLine();
        }
    }
}
