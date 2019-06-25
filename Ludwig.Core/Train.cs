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
                PyObject train = Py.Import("ludwig.train");

                //train(
                //    model_definition, <---- docs forgot to document this positional argument
                //    data_df = None,
                //    data_train_df = None,
                //    data_validation_df = None,
                //    data_test_df = None,
                //    data_csv = None,
                //    data_train_csv = None,
                //    data_validation_csv = None,
                //    data_test_csv = None,
                //    data_hdf5 = None,
                //    data_train_hdf5 = None,
                //    data_validation_hdf5 = None,
                //    data_test_hdf5 = None,
                //    data_dict = None,
                //    train_set_metadata_json = None,
                //    experiment_name = 'api_experiment',
                //    model_name = 'run',
                //    model_load_path = None,
                //    model_resume_path = None,
                //    skip_save_model = False,
                //    skip_save_progress = False,
                //    skip_save_log = False,
                //    skip_save_processed_input = False,
                //    output_directory = 'results',
                //    gpus = None,
                //    gpu_fraction = 1.0,
                //    use_horovod = False,
                //    random_seed = 42,
                //    logging_level = 40,
                //    debug = False
                //)
                var model_definition = Py.CreateScope("asdf").Eval(args.ModelDefinition);
                var arg = new PyTuple(new PyObject[] {model_definition});
                var kwargs = new PyDict();
                if (args.DataCsv != null) kwargs["data_csv"] = Util.ToPython(args.DataCsv);
                //if (order != null) kwargs["order"] = ToPython(order);
                dynamic py = train.InvokeMethod("full_train", arg, kwargs);
            }
        }
    }
}
