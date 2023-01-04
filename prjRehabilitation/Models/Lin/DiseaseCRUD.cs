using prjRehabilitation.ViewModel.Lin;
using prjRehabilitation.Models;
using System.Text.RegularExpressions;

namespace prjRehabilitation.Models.Lin
{
    public class DiseaseCRUD
    {
        public List<VMDisease> search(string search) {
            var db = new dbClassContext();
            IQueryable<DiseaseList> q;
            if (search.All(char.IsAscii))
            {
                q = db.DiseaseLists.Where(x => x.IdDisease.Contains(search));
            }
            else
            {
                q = db.DiseaseLists.Where(x => x.DiseaseChineseName.Contains(search));
            }
            var list = new List<VMDisease>();
            foreach (var c in q.ToList())
            {
                list.Add(new VMDisease
                {
                    DiseaseChineseName = c.DiseaseChineseName,
                    ID_Disease = c.IdDisease
                });
            }
            return list;
        }
    }
}
