using Ludwig.Core;
using Ludwig.Core.AppModels;
using System;

namespace Ludwig.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            var ludwig = new LudwigModel("model_definition.yaml");
            //ludwig.Train(data_csv: "text_classification.csv");
            //ludwig.Save("model");
            ludwig = LudwigModel.Load("model");
            //ludwig.Test("text_classification_predict.csv");
            ludwig.Predict("text_classification_predict.csv");

            Console.ReadLine();
        }
    }
}
