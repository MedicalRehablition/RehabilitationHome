using prjRehabilitation.ViewModel.Lin;
using prjRehabilitation.Models;

namespace prjRehabilitation.Models.Lin
{
    public class DiseaseCRUD
    {
        public List<VMDisease> search(string search) {
            var db = new dbClassContext();
            var q = from c in db.DiseaseLists
                    where c.IdDisease.Contains(search) || c.DiseaseChineseName.Contains(search) 
                    select c;
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
