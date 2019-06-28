using Ludwig.Core.AppModels;
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
            ludwig_model = ludwig_model.InvokeMethod("train", new PyTuple(), kwargs);
        }

        public void Predict(string data_csv)
        {
            var predictions = ludwig_model.predict(data_csv: data_csv);
            //predictions['class']['predictions'];
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
