using Ludwig.Core.AppModels;
using Numpy;
using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Text;
using static Python.Runtime.Py;

namespace Ludwig.Core
{
    public class LudwigModel : IDisposable
    {
        dynamic ludwig_model;

        public LudwigModel(dynamic lud)
        {
            ludwig_model = lud;
        }

        public LudwigModel(string model_definition_file)
        {
            using (GIL())
            {
                PyObject api = Import("ludwig.api");
                var args = Util.ToTuple(new object[]
                    {
                        model_definition_file
                    });
                var kwargs = new PyDict();
                kwargs["model_definition_file"] = Util.ToPython(model_definition_file);
                ludwig_model = api.InvokeMethod("LudwigModel", args, kwargs);
            }
        }

        public void Train(string data_csv = null, LogLevel logging_level = LogLevel.INFO)
        {
            var kwargs = new PyDict();
            if (data_csv != null) kwargs["data_csv"] = Util.ToPython(data_csv);
            kwargs["logging_level"] = Util.ToPython(logging_level);
            ludwig_model.InvokeMethod("train", new PyTuple(), kwargs);
        }

        /// <summary>
        /// This function allows to save models on disk
        /// </summary>
        /// <param name="save_path">
        /// path to the directory where the model is going to be saved. 
        /// Both a JSON file containing the model architecture hyperparameters and checkpoints files containing model weights will be saved.
        /// </param>
        public void Save(string save_path)
        {
            ludwig_model.save(save_path);
        }

        public void Test(string data_csv = null)
        {
            var predictions = ludwig_model.test(data_csv: data_csv);
            var class_predictions = predictions.class_predictions.values;
            Console.WriteLine(class_predictions);
            var class_probability = predictions.class_probability.values;
            Console.WriteLine(class_probability);
        }

        public void TrainOnline()
        {

        }

        public (NDarray<string>, NDarray<float>) Predict(string data_csv)
        {
            var predictions = ludwig_model.predict(data_csv: data_csv);
            var class_predictions = new NDarray<string>(predictions.class_predictions.values);
            var class_probability = new NDarray<float>(predictions.class_probability.values);
            return (class_predictions, class_probability);
        }

        public static LudwigModel Load(string model_dir)
        {
            using(GIL())
            {
                dynamic api = PythonEngine.ImportModule("ludwig.api");
                var lud = api.LudwigModel.load(model_dir);

                return new LudwigModel(lud);
            }
        }

        public void Dispose()
        {
            ludwig_model.close();
        }
    }
}
