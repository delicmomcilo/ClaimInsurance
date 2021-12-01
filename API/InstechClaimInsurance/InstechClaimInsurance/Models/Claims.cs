using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClaimHandlingAPI.Models
{
    public enum TypeEnum
    {
        [Display(Name = "Collision")]
        Collision = 1,
        [Display(Name = "Grounding")]
        Grounding = 2,
        [Display(Name = "Bad Weather")]
        BadWeather = 3,
        [Display(Name = "Fire")]
        Fire = 4
    }

    public class Claim
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "year")]
        public int Year { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "damagecost")]
        [Range(0, 100.000, ErrorMessage = "Damage Cost cannot exceed 100.000")]
        public decimal DamageCost { get; set; }

        [JsonProperty(PropertyName = "type")]
        [EnumDataType(typeof(TypeEnum))]
        public TypeEnum Type
        {
            get; set;
        }
    }
}