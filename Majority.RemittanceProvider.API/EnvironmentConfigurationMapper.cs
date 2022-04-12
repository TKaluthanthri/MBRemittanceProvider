using System;
using System.Collections.Generic;
using System.Linq;

namespace Majority.RemittanceProvider.API
{
    public class EnvironmentConfigurationMapper
    {
        public T BuildConfigurationFromEV<T>()
        {
            Type type = typeof(T);
            List<string> toResolve = GetEnvVariablesToResolve(type);
            string envTest = toResolve.Select(x => x + "=DefaultValue\n").Aggregate((i, j) => i + j);
            Dictionary<string, string> resolvedEV = ResolveEnvironmentVariables(toResolve);
            var config = CreateConfiguration<T>(resolvedEV);
            return config;
        }
        public T BuildConfigurationFromEVForInstance<T>(T instance)
        {
            Type type = typeof(T);
            List<string> toResolve = GetEnvVariablesToResolve(type);
            string envTest = toResolve.Select(x => x + "=DefaultValue\n").Aggregate((i, j) => i + j);
            Dictionary<string, string> resolvedEV = ResolveEnvironmentVariables(toResolve);
            var config = CreateConfigurationForInstance<T>(instance, resolvedEV);
            return config;
        }
        private T CreateConfiguration<T>(Dictionary<string, string> resolvedEV)
        {
            Dictionary<string, string> _lObjects = resolvedEV;
            var class1 = (T)Activator.CreateInstance(typeof(T));
            var class1Type = typeof(T);
            foreach (var _pair in _lObjects)
            {
                var property = class1Type.GetProperty(_pair.Key);
                if (property.PropertyType == typeof(bool) && bool.TryParse(_pair.Value, out var boolValue))
                {
                    property.SetValue(class1, boolValue);
                }
                else if (property.PropertyType == typeof(int) && int.TryParse(_pair.Value, out var intValue))
                {
                    property.SetValue(class1, intValue);
                }
                else if (property.PropertyType == typeof(long) && long.TryParse(_pair.Value, out var longValue))
                {
                    property.SetValue(class1, longValue);
                }
                else if (property.PropertyType == typeof(string))
                {
                    property.SetValue(class1, _pair.Value);
                }
            }
            return class1;
        }
        private T CreateConfigurationForInstance<T>(T instance, Dictionary<string, string> resolvedEV)
        {
            Dictionary<string, string> _lObjects = resolvedEV;
            var class1Type = typeof(T);
            foreach (var _pair in _lObjects)
            {
                var property = class1Type.GetProperty(_pair.Key);
                if (property.PropertyType == typeof(bool) && bool.TryParse(_pair.Value, out var boolValue))
                {
                    property.SetValue(instance, boolValue);
                }
                else if (property.PropertyType == typeof(int) && int.TryParse(_pair.Value, out var intValue))
                {
                    property.SetValue(instance, intValue);
                }
                else if (property.PropertyType == typeof(long) && long.TryParse(_pair.Value, out var longValue))
                {
                    property.SetValue(instance, longValue);
                }
                else if (property.PropertyType == typeof(string))
                {
                    property.SetValue(instance, _pair.Value);
                }
            }
            return instance;
        }
        private Dictionary<string, string> ResolveEnvironmentVariables(List<string> toResolve)
        {
            Dictionary<string, string> re = new Dictionary<string, string>();
            toResolve.ForEach(v =>
            {
                var envValue = Environment.GetEnvironmentVariable(v);
                if (string.IsNullOrEmpty(envValue))
                {
                    envValue = "ENVIRONMENT_VARIABLE_NOT_AVALIABLE";
                }
                re.Add(v, envValue);
            });
            return re;
        }
        private List<string> GetEnvVariablesToResolve(Type type)
        {
            var properties = type.GetProperties();
            return properties.ToList().Select(x => x.Name).ToList();
        }
    }
}
