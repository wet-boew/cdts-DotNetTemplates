namespace GoC.WebTemplate.Components.JSONSerializationObjects
{
    public class IntranetTitle : Link
    {
        public IntranetTitle() { }
        public IntranetTitle(Link link)
        {
            Href = link.Href;
            Text = link.Text;
            Acronym = link.Acronym;
        }

        public string BoldText { get; set; }
    }
}
