
#region Main
int[,] LeTerrain = ConstructionTerrain();

//Liste stockant toutes les cellules à traité au fur et mesure de la découverte de l'environnement par le programme
List<Cellule> FileAttente = new List<Cellule>();

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

        ParcoursVoisin(CellATraite, LeTerrain);


        //Si le voisin est accessible à un cout plus faible -> Sol avoir une liste de toute les cellules parcours en mémoire
            //Pas parcouru
        //Sinon
            //Ajout voisin dans file attente

            //A REFLECHIR
            //Si le voisin accessible à un moindre cout
                //Modifier le predecesseur enregistrer pour le voisin
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
void ParcoursVoisin(Cellule CellActuel, int[,]LeTerrain)
{
    //Note : ajouter toute les cellule créé à une liste et faire retourner cette liste de voisin

    int PositionX;
    int PositionY;

    PositionX = CellActuel.GetX;
    PositionY = CellActuel.GetY;

    //POSSIBILITE D'OPTIMISER CE CODE CAR REPETITIF

    //Vérification du voisin SUD
    if (VerificationConditionChemin(LeTerrain[PositionX, PositionY - 1]))
    {
        //Si le voisin n'est pas le prédécesseur de la cellule que nous traitons
        if( (PositionX != CellActuel.GetParent.GetX) && (PositionY -1 != CellActuel.GetParent.GetY) )
        {
            //On crée la cellule selon les coordonnées du voisin SUD avec le cout (cout = cout CelleActuel + 1 + la valeur du voisin (0,1,2,3))
            Cellule CellSud = new Cellule(PositionX, PositionY - 1, CalculCout(CellActuel.GetCout, LeTerrain[PositionX, PositionY - 1]));
        }
    }

    //Vérification du voisin NORD
    if (VerificationConditionChemin(LeTerrain[PositionX, PositionY + 1]))
    {
        //Si le voisin n'est pas le prédécesseur de la cellule que nous traitons
        if ((PositionX != CellActuel.GetParent.GetX) && (PositionY + 1 != CellActuel.GetParent.GetY))
        {
            //On crée la cellule selon les coordonnées du voisin NORD avec le cout (cout = cout CelleActuel + 1 + la valeur du voisin (0,1,2,3))
            Cellule CellNord = new Cellule(PositionX, PositionY + 1, CalculCout(CellActuel.GetCout, LeTerrain[PositionX, PositionY + 1]));
        }
    }

    //Vérification du voisin EST
    if (VerificationConditionChemin(LeTerrain[PositionX + 1, PositionY]))
    {
        //Si le voisin n'est pas le prédécesseur de la cellule que nous traitons
        if ((PositionX +1 != CellActuel.GetParent.GetX) && (PositionY != CellActuel.GetParent.GetY))
        {
            //On crée la cellule selon les coordonnées du voisin EST avec le cout (cout = cout CelleActuel + 1 + la valeur du voisin (0,1,2,3))
            Cellule CellEst = new Cellule(PositionX + 1, PositionY, CalculCout(CellActuel.GetCout, LeTerrain[PositionX + 1, PositionY]));
        }
    }

    //Vérification du voisin OUEST
    if (VerificationConditionChemin(LeTerrain[PositionX - 1, PositionY]))
    {
        //Si le voisin n'est pas le prédécesseur de la cellule que nous traitons
        if ((PositionX - 1 != CellActuel.GetParent.GetX) && (PositionY != CellActuel.GetParent.GetY))
        {
            //On crée la cellule selon les coordonnées du voisin OUEST avec le cout (cout = cout CelleActuel + 1 + la valeur du voisin (0,1,2,3))
            Cellule CellOuest = new Cellule(PositionX - 1, PositionY, CalculCout(CellActuel.GetCout, LeTerrain[PositionX - 1, PositionY]));
        }
    }
}

//Vérifie si la cellule est accessible (valeur non égale à -1)
bool VerificationConditionChemin(int ValeurCellule)
{
    bool CellAccessible = true;

    if (ValeurCellule == -1)
    {
        CellAccessible = false;
    }

    return CellAccessible;
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