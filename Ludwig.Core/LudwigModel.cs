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
        GILState gil;
        PyObject ludwig;

        public LudwigModel(string model_definition_file)
        {
            gil = GIL();
            var api = Import("ludwig.api");
            var args = Util.ToTuple(new object[]
                {
                    model_definition_file
                });
            var kwargs = new PyDict();
            kwargs["model_definition_file"] = Util.ToPython(model_definition_file);
            ludwig = api.InvokeMethod("LudwigModel", args, kwargs);
        }

        public void train(string data_csv = null, LogLevel logging_level = LogLevel.INFO)
        {
            var kwargs = new PyDict();
            if (data_csv != null) kwargs["data_csv"] = Util.ToPython(data_csv);
            kwargs["logging_level"] = Util.ToPython(logging_level);
            ludwig = ludwig.InvokeMethod("train", new PyTuple(), kwargs);
        }

        public void Dispose()
        {
            gil.Dispose();
        }
    }
}
