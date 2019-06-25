using Ludwig.Core.AppModels;
using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ludwig.Core
{
    public class Train
    {
        public void FullTrain(TrainArgument args)
        {
            using (Py.GIL())
            {
                dynamic train = Py.Import("ludwig.train");

                var pyargs = Util.ToTuple(new object[]
                {
                    args.ModelDefinition
                });
                var kwargs = new PyDict();
                if (args.DataCsv != null) kwargs["data_csv"] = Util.ToPython(args.DataCsv);
                //if (order != null) kwargs["order"] = ToPython(order);
                dynamic py = train.InvokeMethod("full_train", pyargs, kwargs);
            }
        }
    }
}
