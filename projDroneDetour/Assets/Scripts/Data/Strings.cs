using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Strings
{
    public static string tutorial = "Passe por dentro das casas e evite colidir em paredes! e você subir demais, o drone desligará!";
    public static string dontDisplay = "Não exibir mais";

    public static string pressToStart = "Começar";

    public static string bestScore = "Melhor pontuação: ";
    public static string restart = "Recomeçar";
    public static string menu = "Menu";
    public static string ad = "Ver um anúncio e reviver";

    public static string pause = "Pausa";

    public static string wantAd = "Deseja ver um anúncio e retomar ao jogo com a mesma pontuação?";
    public static string offline = "Ops! Parece que você está offline!";

    public static string pressStart = "Aperte em qualquer lugar";
    public static string start = "Jogar";
    public static string options = "Opções";
    public static string credits = "Créditos";

    public static string sound = "Som:";
    public static string language = "Idioma:";
    public static string txtTutorial = "Tutorial:";
    public static string cleanStatistic = "Limpar estatísticas";
    public static string statistic = "Estatísticas";

    public static string nDeaths = "Número de mortes: ";
    public static string nDeathsOstacles = "Mortes por prédios: ";
    public static string nDeathsFall = "Mortes por quedas: ";
    public static string nClicks = "Número de cliques: ";

    public static string deleteStatistics = "Tem certeza que deseja limpar as estatísticas?";

    public static string dev = "Desenvolvedor:";
    public static string music = "Músicas:";
    public static string translator = "Tradutora:";
    public static string distribution = "Distribuição:";

    public static void Translate(string translation)
    {
        if (translation == "English") ChangeToEnglish();
        else if (translation == "Português") ChangeToPortuguese();
        else if (translation == "Español") ChangeToSpanish();
    }

    static void ChangeToEnglish()
    {
        tutorial = "Pass through the houses and avoid hitting the walls! If you are too high, the drone will shut down!";
        dontDisplay = "Don't show";

        pressToStart = "Start";

        bestScore = "High score: ";
        restart = "Restart";
        menu = "Menu";
        ad = "Watch an Ad and revive";

        pause = "Pause";

        wantAd = "Do you want to watch an ad and resume the game with the same score?";
        offline = "It looks like you're offline!";

        pressStart = "Touch the screen";
        start = "Play";
        options = "Options";
        credits = "Credits";

        sound = "Sound:";
        language = "Language:";
        cleanStatistic = "Clear statistics";
        statistic = "Statistics";

        nDeaths = "Deaths: ";
        nDeathsOstacles = "Crashes: ";
        nDeathsFall = "Falls: ";
        nClicks = "Clicks: ";

        deleteStatistics = "Are you sure you want to clear the statistics?";

        dev = "Developer:";
        music = "Music:";
        translator = "Translator:";
        distribution = "Distribution:";
    }

    static void ChangeToPortuguese()
    {
        tutorial = "Passe por dentro das casas e evite colidir em paredes! se você subir demais, o drone desligará!";
        dontDisplay = "Não exibir mais";

        pressToStart = "Começar";

        bestScore = "Melhor pontuação: ";
        restart = "Recomeçar";
        menu = "Menu";
        ad = "Ver um anúncio e reviver";

        pause = "Pausa";

        wantAd = "Deseja ver um anúncio e retomar ao jogo com a mesma pontuação?";
        offline = "Ops! Parece que você está offline!";

        pressStart = "Aperte em qualquer lugar";
        start = "Jogar";
        options = "Opções";
        credits = "Créditos";

        sound = "Som:";
        language = "Idioma:";
        cleanStatistic = "Limpar estatísticas";
        statistic = "Estatísticas";

        nDeaths = "Mortes: ";
        nDeathsOstacles = "Batidas: ";
        nDeathsFall = "Quedas: ";
        nClicks = "Cliques: ";

        deleteStatistics = "Tem certeza que deseja limpar as estatísticas?";

        dev = "Desenvolvedor:";
        music = "Músicas:";
        translator = "Tradutora:";
        distribution = "Distribuição:";
    }

    static void ChangeToSpanish()
    {
        tutorial = "¡Entra en las casas y evita chocar contra las paredes!  ¡Si sube demasiado alto, el drone se apagará!";
        dontDisplay = "No mostrar más";

        pressToStart = "Comenzar";

        bestScore = "Mejor puntuación: ";
        restart = "Recomenzar";
        menu = "Menú";
        ad = "Ver un anuncio y revivir";

        pause = "Pausa";

        wantAd = "¿Quieres ver un anuncio y reanudar el juego con la misma puntuación?";
        offline = "¡Ops! ¡Parece que no estás conectado!";

        pressStart = "Apriete en cualquier lugar";
        start = "Jugar ";
        options = "Opciones ";
        credits = "Créditos";

        sound = "Sonido:";
        language = "Idioma:";
        cleanStatistic = "Limpiar Estadísticas";
        statistic = "Estadísticas";

        nDeaths = "Muertes: ";
        nDeathsOstacles = "Choques: ";
        nDeathsFall = "Caídas: ";
        nClicks = "Clics: ";

        deleteStatistics = "¿Está seguro de que desea borrar las estadísticas?";

        dev = "Desarrollador:";
        music = "Músicas:";
        translator = "Traductor:";
        distribution = "Distribución:";
    }
}
