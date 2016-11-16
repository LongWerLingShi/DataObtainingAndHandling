using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolrNet;
using SolrNet.Attributes;

namespace Pipeline
{
    class SolrData
    {
        [SolrUniqueKey("id")]
        public string id { get; set; }

        //[SolrField("owner_s")]
        //public string owner { get; set; }

        //[SolrField("view_count_i")]
        //public int view_count { get; set; }

        //[SolrField("answer_count_i")]
        //public int answer_count { get; set; }

        [SolrField("creation_date")]
        public string creation_date { get; set; }

        [SolrField("links")]
        public string links { get; set; }

        [SolrField("title")]
        public string title { get; set; }

        [SolrField("content")]
        public string content { get; set; }

        [SolrField("keywords")]
        public string keywords { get; set; }
    }
}
