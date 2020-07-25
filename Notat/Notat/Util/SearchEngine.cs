using Gma.DataStructures.StringSearch;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notat.Util
{
    class SearchEngine<T>
    {
        private SuffixTrie<T> trie;
        private List<T> items;

        public SearchEngine(int minSuffixLength)
        {
            this.trie = new SuffixTrie<T>(minSuffixLength);
        }





    }
}
