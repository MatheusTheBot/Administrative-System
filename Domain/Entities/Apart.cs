namespace Domain.Entities;

public class Apart
{
    public Apart(int number, int block)
    {
        Number = number;
        Block = block;
    }

    public Apart()
    {

    }

    private readonly List<Resident> _Residents = new();
    private readonly List<Packages> _Packages = new();
    private readonly List<Visitant> _Visitants = new();

    public int Number { get; private set; }
    public int Block { get; private set; }
    public List<Resident> Residents { get => _Residents; }
    public List<Packages> Packages { get => _Packages; }
    public List<Visitant> Visitants { get => _Visitants; }

    public void AddResident(Resident newResident)
    {
        _Residents.Add(newResident);
    }
    public void RemoveResident(Guid Id)
    {
        var res = _Residents.Find(x => x.Id == Id);
        if (res != null)
            _Residents.Remove(res);
    }
    public void AddPackage(Packages newPackage)
    {
        _Packages.Add(newPackage);
    }
    public void RemovePackage(Guid Id)
    {
        var pack = _Packages.Find(x => x.Id == Id);
        if (pack != null)
            _Packages.Remove(pack);
    }
    public void AddVisitant(Visitant newVisitant)
    {
        _Visitants.Add(newVisitant);
    }
    public void RemoveVisitant(Guid Id)
    {
        var vis = _Visitants.Find(x => x.Id == Id);
        if (vis != null)
            _Visitants.Remove(vis);
    }
}