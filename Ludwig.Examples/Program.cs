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
            ludwig.train(data_csv: "text_classification.csv");
        }
    }
}
