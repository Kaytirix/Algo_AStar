
int[,] LeTerrain = ConstructionTerrain();

//Liste stockant toutes les cellules à traité au fur et mesure de la découverte de l'environnement par le programme
List<Cellule> FileAttente = new List<Cellule>();

//Cout estimer selon la distance de manhattan (Ne prend pas en compte la diagonale)
int CoutEstimer;

//Définition du départ et de l'arriver
Cellule Depart = new Cellule(0, 0, 0);
Cellule Arriver = new Cellule(7, 9, 0);

//Ajoute la cellule de départ à la file d'attente pour indiquer au programme où il commence
FileAttente.Add(Depart);

CoutEstimer = CalculHeuristique(Depart, Arriver);

//Algo A*
while(FileAttente.Count > 0)
{
    DetermineCellPrioritaire();
}



static void CalculCout()
{
    
}

static void CalculProrite()
{

}

//Calcul l'heuristi de la carte
static int CalculHeuristique(Cellule Depart, Cellule Arriver)
{
    int CoutEstimer;

    CoutEstimer = (Arriver.GetX - Depart.GetX) + (Arriver.GetY - Depart.GetY);

    return CoutEstimer;
}

static void TrouverVoisin()
{

}

static void VerificationConditionChemin()
{

}

static void DetermineCellPrioritaire()
{

    /*
    Cellule CellMaxCout;

    CellMaxCout = FileAttente[0];

    foreach(Cellule Cell in FileAttente)
    {
        if (Cell.GetCout >= CellMaxCout.GetCout)
        {
            CellMaxCout = Cell;
        }
    }

    return CellMaxCout;
    */
}

static void ReconstitutionChemin()
{

}

static int[,] ConstructionTerrain()
{
    int[,] Terrain;

    Terrain =new int[,] {
        { 0,0,0,0,0,0,0,0,-1},
        { 0,0,1,2,2,0,0,-1,0},
        { 0,0,1,1,2,0,-1,0,0},
        { 0,0,0,0,0,0,0,0,0},
        { 0,0,3,3,3,0,0,0,0},
        { 0,2,2,3,0,0,-1,-1,0},
        { 2,1,1,0,0,0,0,0,0}
    };

    return Terrain;
}

static void ParcoursTableau()
{

}

public class Cellule
{
    //Attribut
    private int x;
    private int y;
    private Cellule? CellParent;
    private float Cout;

    public Cellule(int x, int y, float cout)
    {
        this.x = x;
        this.y = y;
        Cout = cout;
    }

    public float GetCout { get; set; }

    public int GetX { get; set; }

    public int GetY { get; set; }
}