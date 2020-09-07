using Hsf.ApplicationProcess.August2020.Domain.Extensions;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Hsf.ApplicationProcess.August2020.Blazor.ApiServices
{
    public class ResponseCodes : IEnumerable<KeyValuePair<string, List<string>>>
    {
        private readonly ConcurrentDictionary<string, List<string>> _infos = new ConcurrentDictionary<string, List<string>>();

        public ResponseCodes AddCode(string name, params string[] infos)
        {
            if (infos.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(infos));

            foreach (var info in infos)
                _infos.AddOrUpdate(name, _ => AddFactory(info), (_, l) => UpdateFactory(info, l));

            return this;
        }

        private List<string> AddFactory(string additionalCode)
        {
            return new List<string> { additionalCode };
        }

        private List<string> UpdateFactory(string additionalCode, List<string> currentList)
        {
            currentList.Add(additionalCode);
            return currentList;
        }

        public IEnumerable<IEnumerable<string>> GetCodes()
        {
            return new List<List<string>>(_infos.Values);
        }

        public IEnumerator<KeyValuePair<string, List<string>>> GetEnumerator()
        {
            return _infos.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}