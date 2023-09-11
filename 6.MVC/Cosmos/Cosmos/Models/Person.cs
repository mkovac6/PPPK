using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cosmos.Models
{
    public class Person
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [Required]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [Required]
        [JsonProperty(PropertyName = "jobOccupation")]
        public string JobOccupation { get; set; }

        [JsonProperty(PropertyName = "isAdult")]
        public bool Adult { get; set; }
    }
}