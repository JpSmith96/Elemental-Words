using ElementalWords.Classes.Element;

namespace ElementalWords.Services
{
    public interface IElementService
    {
        bool InitializeElements();

        ICollection<ElementResponse> ElementalForms(string word);
    }
}
