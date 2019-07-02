using Ludwig.Core;
using Ludwig.Core.AppModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ludwig.UnitTest
{
    [TestClass]
    public class TrainTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var train = new Train();
            train.FullTrain(new LudwigArgs
            {
                DataCsv = "text_classification.csv",
                ModelDefinition = "{input_features: [{name: doc_text, type: text}], output_features: [{name: class, type: category}]}",
                ModelDefinitionFile = "model_definition.yaml"
            });
        }
    }
}

