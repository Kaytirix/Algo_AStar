
#region Main
int[,] LeTerrain = ConstructionTerrain();

//Liste stockant toutes les cellules à traité au fur et mesure de la découverte de l'environnement par le programme
List<Cellule> FileAttente = new List<Cellule>();


List<Cellule> ListVoisin = new List<Cellule>();

//Cout estimer selon la distance de manhattan (Ne prend pas en compte la diagonale)
int CoutEstimer;

Cellule CellATraite;

//Définition du départ et de l'arriver
Cellule Depart = new Cellule(0, 0, 0);
Cellule Arriver = new Cellule(7, 9, 0);

//Ajoute la cellule de départ à la file d'attente pour indiquer au programme où il commence
FileAttente.Add(Depart);

CoutEstimer = CalculHeuristique(Depart, Arriver);

//Algo A* (A star)
while(FileAttente.Count > 0)
{
    //Détermine quelle cellule est à traité en premier
    CellATraite = DetermineCellPrioritaire(CoutEstimer,FileAttente);

    //Supprime la cellule en cours de traitement de la file d'attente
    FileAttente.Remove(CellATraite);

    //Si le programme atteint l'arriver
    if (CellATraite.Equals(Arriver))
    {
        //ReconstitutionChemin()
        break;
    }
    else
    {

        ListVoisin = ParcoursVoisin(CellATraite, LeTerrain);

        if(ListVoisin.Count > 0)
        {
            //Si le voisin est accessible à un cout plus faible -> Sol avoir une liste de toute les cellules parcours en mémoire
            //Pas parcouru
        //Sinon
            //Ajout voisin dans file attente

            //A REFLECHIR
            //Si le voisin accessible à un moindre cout
                //Modifier le predecesseur enregistrer pour le voisin
        }

        
    }
}
#endregion


//Calcul le cout d'une cellule
static int CalculCout(int CoutActuel, int ValeurCellule)
{
    return CoutActuel +1 + ValeurCellule;
}

//Calcul la priorité d'une cellule
int CalculProrite(int CoutCell, int CoutEstimer)
{
    return CoutCell + CoutEstimer;
}

//Calcul l'heuristi de la carte
static int CalculHeuristique(Cellule Depart, Cellule Arriver)
{
    int CoutEstimer;

    CoutEstimer = (Arriver.GetX - Depart.GetX) + (Arriver.GetY - Depart.GetY);

    return CoutEstimer;
}

//Liste tout les voisins autour de la cellule actuel
List<Cellule> ParcoursVoisin(Cellule CellActuel, int[,]LeTerrain)
{
    List<Cellule> ListVoisin = new List<Cellule>();

    int PositionX;
    int PositionY;

    PositionX = CellActuel.GetX;
    PositionY = CellActuel.GetY;

    //Vérification du voisin SUD
    VerificationConditionChemin(LeTerrain,PositionX, PositionY - 1, CellActuel, ListVoisin);

    //Vérification du voisin NORD
    VerificationConditionChemin(LeTerrain,PositionX, PositionY + 1, CellActuel, ListVoisin);

    //Vérification du voisin EST
    VerificationConditionChemin(LeTerrain, PositionX + 1, PositionY, CellActuel, ListVoisin);

    //Vérification du voisin OUEST
    VerificationConditionChemin(LeTerrain, PositionX - 1, PositionY, CellActuel, ListVoisin);

    return ListVoisin;
}

//Vérifie si la cellule est accessible (valeur non égale à -1)
void VerificationConditionChemin(int[,] LeTerrain, int PositionX, int PositionY, Cellule CellActuel,List<Cellule> ListVoisin)
{

    if (LeTerrain[PositionX, PositionY] != -1)
    {
        //Si le voisin n'est pas le prédécesseur de la cellule que nous traitons
        if ((PositionX != CellActuel.GetParent.GetX) && (PositionY != CellActuel.GetParent.GetY))
        {
            //On crée la cellule selon les coordonnées du voisin avec le cout (cout = cout CelleActuel + 1 + la valeur du voisin (0,1,2,3))
            Cellule CellVoisin = new Cellule(PositionX, PositionY, CalculCout(CellActuel.GetCout, LeTerrain[PositionX, PositionY]));
            ListVoisin.Add(CellVoisin);
        }
    }
}

//Détermine, parmis toutes les cellules à traité, la quelle est prioritaire
//Si plusieurs cellules possèdent la même priorité, c'est la dernière qui est enregistré
Cellule DetermineCellPrioritaire(int CoutEstimer, List<Cellule> FileAttente)
{
    Cellule CellMaxCout;

    CellMaxCout = FileAttente[0];

    foreach(Cellule Cell in FileAttente)
    {
        if ( ( CalculProrite(Cell.GetCout,CoutEstimer) ) >= (CalculProrite(CellMaxCout.GetCout, CoutEstimer) ))
        {
            CellMaxCout = Cell;
        }
    }
    
    return CellMaxCout; 
}

static void ReconstitutionChemin()
{

}

//Construit le terrain permettant d'effectuer les tests
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

#region Class Cellule
public class Cellule
{
    //Attribut
    private int x;
    private int y;
    private Cellule? CellParent;
    private int Cout;

    public Cellule(int x, int y, int cout)
    {
        this.x = x;
        this.y = y;
        Cout = cout;
    }

    public int GetCout { get; set; }

    public int GetX { get; set; }

    public int GetY { get; set; }

    public Cellule GetParent { get; set; }
}
#endregion