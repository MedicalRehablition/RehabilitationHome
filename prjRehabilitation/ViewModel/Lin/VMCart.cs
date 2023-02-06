using prjRehabilitation.Models;

namespace prjRehabilitation.ViewModel.Lin
{
	public class VMCart
	{
		public VMCart() {
			Item = new List<int>();
		}
		public List<int> Item{get; set;}
	}
}
