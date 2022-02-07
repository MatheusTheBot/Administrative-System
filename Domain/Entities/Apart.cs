namespace Domain.Entities;

public class Apart
{
    public Apart(int number, int block)
    {
        Number = number;
        Block = block;
    }

    private List<Resident> _Residents = new();
    private List<Packages> _Packages = new();
    private List<Visitant> _Visitants = new();

    public int Number { get; private set; }
    public int Block { get; private set; }
    public List<Resident> Residents { get => _Residents; }
    public List<Packages> Packages { get => _Packages; }
    public List<Visitant> Visitants { get => _Visitants; }

    public void AddResident(Resident newResident) 
    {
        _Residents.Add(newResident);
    }
    public void RemoveResident(Resident resident) 
    {
        _Residents.Remove(resident);
    }
    public void AddPackage(Packages newPackage) 
    {
        _Packages.Add(newPackage);
    }
    public void RemovePackage(Packages package) 
    {
        _Packages.Remove(package);
    }
    public void AddVisitant(Visitant newVisitant) 
    {
        _Visitants.Add(newVisitant);
    }
    public void RemoveVisitant(Visitant visitant) 
    {
        _Visitants.Remove(visitant);
    }
}