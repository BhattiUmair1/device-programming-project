using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Models
{
    class Registration : TableEntity
    {
        public Registration()
        {

        }
        public Registration(string partitionKey, Guid rowKey)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = rowKey.ToString();
        }

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
