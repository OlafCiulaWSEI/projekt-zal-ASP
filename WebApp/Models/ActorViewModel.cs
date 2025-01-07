public class ActorViewModel
{
    public int PersonId { get; set; } // ID aktora
    public string PersonName { get; set; } // Imię i nazwisko aktora
    public int MovieCount { get; set; } // Liczba filmów, w których aktor występował
    public List<MovieRoleViewModel> Movies { get; set; } // Lista ról aktora w filmach
}