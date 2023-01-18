using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zephry
{
    public class EmailBody: IEnumerable<string>
    {
        private readonly List<string> _paragraphs = new List<string>();

        public void Clear() {
            _paragraphs.Clear();
        }

        public void Add(string aString)
        {
            _paragraphs.Add(aString);
        }

        public IEnumerator<String> GetEnumerator()
        {
            return _paragraphs.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _paragraphs.GetEnumerator();
        }
    }
}
