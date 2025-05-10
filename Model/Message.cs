using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Model
{
    public class Message
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        public string SenderId {get; set; }
        public string ReceiverId {get;  set;}
        public string ChatRoom {get; set;}
        public string Content {get; set;}
        public DateTime Timestamp {get; set; }
    }
}