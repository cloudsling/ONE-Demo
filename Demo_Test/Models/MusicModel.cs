using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class MusicModel
    {
        public MusicModel()
        {
            Author = new PeopleModel();
        }

        public string Id { get; set; }

        public string MusicId { get; set; }

        public string Cover { get; set; }

        public string Title { get; set; }

        public string Platform { get; set; }

        public PeopleModel Author { get; set; }
    }

}
