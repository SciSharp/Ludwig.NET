using Ludwig.Core.AppModels;
using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Text;
using static Python.Runtime.Py;

namespace Ludwig.Core
{
    public class Util
    {
        public static PyTuple ToTuple(Array input)
        {
            var array = new PyObject[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                array[i] = ToPython(input.GetValue(i));
            }
            return new PyTuple(array);
        }

        public static PyObject ToPython(object obj)
        {
            if (obj == null) return Runtime.GetPyNone();
            switch (obj)
            {
                // basic types
                case int o: return new PyInt(o);
                case long o: return new PyLong(o);
                case float o: return new PyFloat(o);
                case double o: return new PyFloat(o);
                case string o: return new PyString(o);
                case bool o: return ConverterExtension.ToPython(o);
                case PyObject o: return o;
                case LogLevel o: return new PyInt((int)o);
                // sequence types
                case Array o: return ToTuple(o);
                default: throw new NotImplementedException($"Type is not yet supported: { obj.GetType().Name}. Add it to 'ToPythonConversions'");
            }
        }
    }
}
