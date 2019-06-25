using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ludwig.Core.AppModels
{
    public class TrainArgument
    {
        [DisplayName("output_directory")]
        [DefaultValue("results")]
        public string OutputDirectory { get; set; }

        [DisplayName("data_csv")]
        public string DataCsv { get; set; }

        [DisplayName("model_definition")]
        [Description("input data CSV file. " +
            "If it has a split column, it will be used for splitting " +
            "(0: train, 1: validation, 2: test), " +
            "otherwise the dataset will be randomly split")]
        public string ModelDefinition { get; set; }
    }
}
