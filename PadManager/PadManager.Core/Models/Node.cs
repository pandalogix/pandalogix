using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace PadManager.Core.Models
{
    public class Node : BaseEntity
    {
        public Pad Pad { get; set; }

        public int NodeId { get; set; }

        [NotMapped]
        public List<long> InNodes
        {
            get
            {
                return System.Array.ConvertAll(InNodeList.Split(';'), long.Parse).ToList();
            }
            set
            {
                InNodeList = String.Join(";", value.Select(p => p.ToString()).ToArray());
            }
        }
        [NotMapped]
        public List<long> OutNodes
        {
            get
            {
                return System.Array.ConvertAll(OutNodesList.Split(';'), long.Parse).ToList();
            }
            set
            {
                OutNodesList = String.Join(";", value.Select(p => p.ToString()).ToArray());
            }
        }
        [JsonIgnore]
        [Column("InNodes")]
        public string InNodeList { get; set; }
        [JsonIgnore]
        [Column("OutNodes")]
        public string OutNodesList { get; set; }
        public string MetaData { get; set; }

        public string NodeType { get; set; }

        public string Location { get; set; }
    }
}