using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
using I2.Loc;
using UnityEngine;


public class PagesItemMap : PagesItemBase
{
    [SerializeField]
    private int titleIndex;

    protected override void SetKeys()
    {
        key_info = Constants.key_item_info_map;
        key_lock = Constants.key_item_lock_map; 
    }

    protected override void ShowPoup(int index)
    {
        title = GetTitleText();
        message = GetMessageText();

        bool isButton = Constants.total_coins >= price ? true : false;

        m_popup.GetComponent<ItemsPopup>().Set(index, iconImage.sprite, title, message, price.ToString(), isButton, buttonAction, isShowPriceButton, true, false);
        UIPopupManager.ShowPopup(m_popup, m_popup.AddToPopupQueue, false);
    }


    protected override void UpdateValues(int index)
    {
        Constants.total_coins -= price;
        PlayerPrefs.SetInt(key_lock + index, 1);
        PlayerPrefs.SetInt(key_info + index, 1);
        PlayerPrefs.Save();
    }

    protected override void SelectedItem()
    {

        GameManager.Instance.InfoStatusMapUpdate();
    }


    private string GetTitleText()
    {
        if (titleIndex == 0)
        {
            
            return GetHogwartsExpressTitle();
        }
        else if(titleIndex == 1)
        {
            return GetCaniballiaTitle();
        }
        else if (titleIndex == 2)
        {
            return GetDracultownTitle();
        }
        else if (titleIndex == 3)
        {
            return GetAncientEgyptTitle();
        }
        else if (titleIndex == 4)
        {
            return GetIndiaTitle();
        }
        else if (titleIndex == 5)
        {
            return GetJapanTitle();
        }
        else if (titleIndex == 6)
        {
            return GetEmpireofChinTitle();
        }
        else if (titleIndex == 7)
        {
            return GetArcticTitle();
        }
        else if (titleIndex == 8)
        {
            return GetForestTitle();
        }
        else if (titleIndex == 9)
        {
            return GetJurassicperiodTitle();
        }
        else if (titleIndex == 10)
        {
            return GetSafariTitle();
        }
        else if (titleIndex == 11)
        {
            return GetVormirTitle();
        }

        return "";
    }

    private string GetHogwartsExpressTitle()
    {
        switch (Constants.language_current)
        {
            case "English":
                return "Hogwarts Express";
            case "Russian":
                return "Хогвартс Экспресс";
            case "Spanish":
                return "Expreso de Hogwarts";
            case "Italian":
                return "Espresso di Hogwarts";
            case "German":
                return "Hogwarts-Express";
            case "French":
                return "Poudlard Express";
            case "Portuguese":
                return "Expresso de Hogwarts";
            case "Japanese":
                return "ホグワーツエクスプレス";
            case "Chinese":
                return "霍格沃茨特快列車";
            case "Korean":
                return "호그와트 익스프레스";
            default:
                return "Hogwarts Express";
        }
    }

    private string GetCaniballiaTitle()
    {
        switch (Constants.language_current)
        {
            case "English":
                return "Caniballia";
            case "Russian":
                return "Канибаллия";
            case "Spanish":
                return "Canibalia";
            case "Italian":
                return "Caniballa";
            case "German":
                return "Caniballia";
            case "French":
                return "Caniballia";
            case "Portuguese":
                return "Canibalia";
            case "Japanese":
                return "カニバリア";
            case "Chinese":
                return "卡尼巴利亞";
            case "Korean":
                return "카니발리아";
            default:
                return "Caniballia";
        }
    }

    private string GetDracultownTitle()
    {
        switch (Constants.language_current)
        {
            case "English":
                return "Dracultown";
            case "Russian":
                return "Дракултовн";
            case "Spanish":
                return "Dracultown";
            case "Italian":
                return "Città di Dracul";
            case "German":
                return "Draculstadt";
            case "French":
                return "Dracultown";
            case "Portuguese":
                return "Dracultown";
            case "Japanese":
                return "ドラクルタウン";
            case "Chinese":
                return "龍城";
            case "Korean":
                return "드라큘타운";
            default:
                return "Dracultown";
        }
    }

    private string GetAncientEgyptTitle()
    {
        switch (Constants.language_current)
        {
            case "English":
                return "Ancient Egypt";
            case "Russian":
                return "Древний Египет";
            case "Spanish":
                return "Antiguo Egipto";
            case "Italian":
                return "Antico Egitto";
            case "German":
                return "Antikes Ägypten";
            case "French":
                return "L'Egypte ancienne";
            case "Portuguese":
                return "Antigo Egito";
            case "Japanese":
                return "古代エジプト";
            case "Chinese":
                return "古埃及";
            case "Korean":
                return "고대 이집트";
            default:
                return "Ancient Egypt";
        }
    }

    private string GetIndiaTitle()
    {
        switch (Constants.language_current)
        {
            case "English":
                return "India";
            case "Russian":
                return "Индия";
            case "Spanish":
                return "India";
            case "Italian":
                return "Indio";
            case "German":
                return "Indien";
            case "French":
                return "Inde";
            case "Portuguese":
                return "Índio";
            case "Japanese":
                return "インド";
            case "Chinese":
                return "印度";
            case "Korean":
                return "인도";
            default:
                return "India";
        }
    }

    private string GetJapanTitle()
    {
        switch (Constants.language_current)
        {
            case "English":
                return "Japan";
            case "Russian":
                return "Япония";
            case "Spanish":
                return "Japón";
            case "Italian":
                return "Giappone";
            case "German":
                return "Japan";
            case "French":
                return "Japon";
            case "Portuguese":
                return "Japão";
            case "Japanese":
                return "日本";
            case "Chinese":
                return "日本";
            case "Korean":
                return "일본";
            default:
                return "Japan";
        }
    }

    private string GetEmpireofChinTitle()
    {
        switch (Constants.language_current)
        {
            case "English":
                return "Empire of Chin";
            case "Russian":
                return "Империя Тан";
            case "Spanish":
                return "Imperio de Chin";
            case "Italian":
                return "Impero del mento";
            case "German":
                return "Reich von Chin";
            case "French":
                return "Empire du menton";
            case "Portuguese":
                return "Império de Chin";
            case "Japanese":
                return "あごの帝国";
            case "Chinese":
                return "秦帝國";
            case "Korean":
                return "진 제국";
            default:
                return "Empire of Chin";
        }
    }

    private string GetArcticTitle()
    {
        switch (Constants.language_current)
        {
            case "English":
                return "Arctic";
            case "Russian":
                return "Арктика";
            case "Spanish":
                return "Ártico";
            case "Italian":
                return "Artico";
            case "German":
                return "Arktis";
            case "French":
                return "Arctique";
            case "Portuguese":
                return "Ártico";
            case "Japanese":
                return "北極";
            case "Chinese":
                return "北極";
            case "Korean":
                return "북극";
            default:
                return "Arctic";
        }
    }

    private string GetForestTitle()
    {
        switch (Constants.language_current)
        {
            case "English":
                return "Forest";
            case "Russian":
                return "Лес";
            case "Spanish":
                return "Bosque";
            case "Italian":
                return "Foresta";
            case "German":
                return "Wald";
            case "French":
                return "Forêt";
            case "Portuguese":
                return "Floresta";
            case "Japanese":
                return "森";
            case "Chinese":
                return "森林";
            case "Korean":
                return "숲";
            default:
                return "Forest";
        }
    }

    private string GetJurassicperiodTitle()
    {
        switch (Constants.language_current)
        {
            case "English":
                return "Jurassic period";
            case "Russian":
                return "Юрский период";
            case "Spanish":
                return "Periodo Jurasico";
            case "Italian":
                return "Periodo giurassico";
            case "German":
                return "Jurazeit";
            case "French":
                return "Période jurassique";
            case "Portuguese":
                return "período jurássico";
            case "Japanese":
                return "ジュラ紀";
            case "Chinese":
                return "侏羅紀";
            case "Korean":
                return "쥐라기";
            default:
                return "Jurassic period";
        }
    }

    private string GetSafariTitle()
    {
        switch (Constants.language_current)
        {
            case "English":
                return "Safari";
            case "Russian":
                return "Сафари";
            case "Spanish":
                return "Safari";
            case "Italian":
                return "Safari";
            case "German":
                return "Safari";
            case "French":
                return "Safari";
            case "Portuguese":
                return "Safári";
            case "Japanese":
                return "サファリ";
            case "Chinese":
                return "蘋果瀏覽器";
            case "Korean":
                return "원정 여행";
            default:
                return "Safari";
        }
    }

    private string GetVormirTitle()
    {
        switch (Constants.language_current)
        {
            case "English":
                return "Vormir";
            case "Russian":
                return "Вормир";
            case "Spanish":
                return "Vormir";
            case "Italian":
                return "Vormir";
            case "German":
                return "Vormir";
            case "French":
                return "Vormir";
            case "Portuguese":
                return "Vormir";
            case "Japanese":
                return "ヴォルミール";
            case "Chinese":
                return "沃爾米爾";
            case "Korean":
                return "보르미르";
            default:
                return "Vormir";
        }
    }


    private string GetMessageText()
    {

        if (titleIndex == 0)
        {
            return GetHogwartsExpressMessage();
        }
        else if (titleIndex == 1)
        {
            return GetCaniballiaMessage();
        }
        else if (titleIndex == 2)
        {
            return GetDracultownMessage();
        }
        else if (titleIndex == 3)
        {
            return GetAncientEgyptMessage();
        }
        else if (titleIndex == 4)
        {
            return GetIndiaMessage();
        }
        else if (titleIndex == 5)
        {
            return GetJapanMessage();
        }
        else if (titleIndex == 6)
        {
            return GetEmpireofChinMessage();
        }
        else if (titleIndex == 7)
        {
            return GetArcticMessage();
        }
        else if (titleIndex == 8)
        {
            return GetForestMessage();
        }
        else if (titleIndex == 9)
        {
            return GetJurassicperiodMessage();
        }
        else if (titleIndex == 10)
        {
            return GetSafariMessage();
        }
        else if (titleIndex == 11)
        {
            return GetVormirMessage();
        }

        return "";
    }

    private string GetHogwartsExpressMessage()
    {

        switch (Constants.language_current)
        {
            case "English":
                return "Go through the canyons to Texas by Hogwarts Express.";
            case "Russian":
                return "Проехать через каньоны в Техас на Хогвартс-Экспрессе";
            case "Spanish":
                return "Recorre los cañones hasta Texas en Hogwarts Express.";
            case "Italian":
                return "Attraversa i canyon fino al Texas con l'Hogwarts Express.";
            case "German":
                return "Fahren Sie mit dem Hogwarts Express durch die Schluchten nach Texas.";
            case "French":
                return "Traversez les canyons jusqu'au Texas en Poudlard Express.";
            case "Portuguese":
                return "Atravesse os desfiladeiros até o Texas pelo Expresso de Hogwarts.";
            case "Japanese":
                return "ホグワーツエクスプレスで峡谷を通り抜けてテキサスまで行きます。";
            case "Chinese":
                return "乘坐霍格沃茨特快穿過峽谷前往德克薩斯州。";
            case "Korean":
                return "호그와트 익스프레스를 타고 협곡을 지나 텍사스로 이동합니다.";
            default:
                return "Go through the canyons to Texas by Hogwarts Express.";
        }
    }

    private string GetCaniballiaMessage()
    {

        switch (Constants.language_current)
        {
            case "English":
                return "You can find cannibals in reality, just they are hided from you. ";
            case "Russian":
                return "Вы можете найти каннибалов в реальности, просто они скрыты от вас.";
            case "Spanish":
                return "Puedes encontrar caníbales en la realidad, solo que están ocultos para ti.";
            case "Italian":
                return "Puoi trovare cannibali nella realtà, solo che ti sono nascosti.";
            case "German":
                return "Sie können Kannibalen in der Realität finden, nur sind sie vor Ihnen verborgen.";
            case "French":
                return "Vous pouvez trouver des cannibales dans la réalité, mais ils vous sont cachés.";
            case "Portuguese":
                return "Você pode encontrar canibais na realidade, apenas eles estão escondidos de você.";
            case "Japanese":
                return "あなたは実際に人食い人種を見つけることができます、ただ彼らはあなたから隠されています。";
            case "Chinese":
                return "你可以在現實中找到食人者，只是他們對你隱藏。";
            case "Korean":
                return "식인종은 현실에서 찾을 수 있습니다. 단지 그들이 당신에게서 숨겨져 있을 뿐입니다.";
            default:
                return "You can find cannibals in reality, just they are hided from you.";
        }
    }

    private string GetDracultownMessage()
    {

        switch (Constants.language_current)
        {
            case "English":
                return "Be careful, don't go close to the castle. Dracula is here....";
            case "Russian":
                return "Будьте осторожны, не подходите близко к замку. Дракула здесь...";
            case "Spanish":
                return "Ten cuidado, no te acerques al castillo. Drácula está aquí....";
            case "Italian":
                return "Fai attenzione, non avvicinarti al castello. Dracula è qui....";
            case "German":
                return "Seien Sie vorsichtig, gehen Sie nicht in die Nähe des Schlosses. Dracula ist da....";
            case "French":
                return "Attention, ne vous approchez pas du château. Dracula est là...";
            case "Portuguese":
                return "Tenha cuidado, não se aproxime do castelo. Drácula está aqui....";
            case "Japanese":
                return "注意してください、城に近づかないでください。 ドラキュラはここにあります...";
            case "Chinese":
                return "小心，不要靠近城堡。 德古拉來了……";
            case "Korean":
                return "주의, 성에 가까이 가지 마십시오. 드라큘라가 왔다....";
            default:
                return "Be careful, don't go close to the castle. Dracula is here....";
        }
    }

    private string GetAncientEgyptMessage()
    {

        switch (Constants.language_current)
        {
            case "English":
                return "Find a way through labyrinth of pyramid and kill the enemies.";
            case "Russian":
                return "Найдите путь через лабиринт пирамиды и убейте врагов.";
            case "Spanish":
                return "Encuentra un camino a través del laberinto de la pirámide y mata a los enemigos.";
            case "Italian":
                return "Trova un modo attraverso il labirinto della piramide e uccidi i nemici.";
            case "German":
                return "Finde einen Weg durch das Labyrinth der Pyramide und töte die Feinde.";
            case "French":
                return "Trouvez un chemin à travers le labyrinthe de la pyramide et tuez les ennemis.";
            case "Portuguese":
                return "Encontre um caminho através do labirinto da pirâmide e mate os inimigos.";
            case "Japanese":
                return "ピラミッドの迷宮を通り抜ける方法を見つけて、敵を殺します。";
            case "Chinese":
                return "找到穿過金字塔迷宮的方法並殺死敵人。";
            case "Korean":
                return "피라미드의 미로를 통해 길을 찾아 적을 죽이십시오.";
            default:
                return "Find a way through labyrinth of pyramid and kill the enemies.";
        }
    }

    private string GetIndiaMessage()
    {

        switch (Constants.language_current)
        {
            case "English":
                return "Gold-rich soil... Find the great hidden treasure of raja Maharishi.";
            case "Russian":
                return "Богатая золотом почва... Найдите великое скрытое сокровище раджи Махариши.";
            case "Spanish":
                return "Suelo rico en oro... Encuentra el gran tesoro escondido de raja Maharishi.";
            case "Italian":
                return "Terreno ricco d'oro... Trova il grande tesoro nascosto di raja Maharishi.";
            case "German":
                return "Goldreiche Erde ... Finden Sie den großen verborgenen Schatz von Raja Maharishi.";
            case "French":
                return "Sol riche en or... Trouvez le grand trésor caché du raja Maharishi.";
            case "Portuguese":
                return "Solo rico em ouro... Encontre o grande tesouro escondido de raja Maharishi.";
            case "Japanese":
                return "金が豊富な土壌...ラジャマハリシの素晴らしい隠された宝物を見つけてください。";
            case "Chinese":
                return "富含黃金的土壤......找到 raja Maharishi 的巨大隱藏寶藏。";
            case "Korean":
                return "금이 풍부한 토양... Raja Maharishi의 위대한 숨겨진 보물을 찾으십시오.";
            default:
                return "Gold-rich soil... Find the great hidden treasure of raja Maharishi.";
        }
    }

    private string GetJapanMessage()
    {

        switch (Constants.language_current)
        {
            case "English":
                return "Separate from the world .. mysterious land ... new adventures.";
            case "Russian":
                return "Отдельно от мира.. таинственная земля... новые приключения.";
            case "Spanish":
                return "Separado del mundo... tierra misteriosa... nuevas aventuras.";
            case "Italian":
                return "Separati dal mondo... terra misteriosa... nuove avventure.";
            case "German":
                return "Getrennt von der Welt ... mysteriöses Land ... neue Abenteuer.";
            case "French":
                return "Séparé du monde .. terre mystérieuse ... nouvelles aventures.";
            case "Portuguese":
                return "Separado do mundo... terra misteriosa... novas aventuras.";
            case "Japanese":
                return "世界から離れて..不思議な土地...新しい冒険。";
            case "Chinese":
                return "與世隔絕......神秘的土地......新的冒險。";
            case "Korean":
                return "세계와 분리 .. 신비한 땅 ... 새로운 모험.";
            default:
                return "Separate from the world .. mysterious land ... new adventures.";
        }
    }

    private string GetEmpireofChinMessage()
    {

        switch (Constants.language_current)
        {
            case "English":
                return "Rule of the dynasty Tang. Complete mission of the emperor. Rescue the princess \"Sua Lin\".";
            case "Russian":
                return "Правление династии Тан. Выполнить миссию императора. Спасите принцессу \"Суа Лин\".";
            case "Spanish":
                return "Regla de la dinastía Tang. Misión completa del emperador. Rescata a la princesa \"Sua Lin\".";
            case "Italian":
                return "Regola della dinastia Tang. Completa la missione dell'imperatore. Salva la principessa \"Sua Lin\".";
            case "German":
                return "Herrschaft der Dynastie Tang. Komplette Mission des Kaisers. Rette die Prinzessin \"Sua Lin\".";
            case "French":
                return "Règle de la dynastie Tang. Mission complète de l'empereur. Sauvez la princesse \"Sua Lin\".";
            case "Portuguese":
                return "Regra da dinastia Tang. Missão completa do imperador. Resgate a princesa \"Sua Lin\".";
            case "Japanese":
                return "唐王朝の支配。 皇帝の完全な任務。 王女「スアリン」を救出してください。";
            case "Chinese":
                return "唐朝統治。 完成皇帝的使命。 救出公主“蘇琳”。";
            case "Korean":
                return "당나라의 통치. 황제의 임무를 완수하십시오. 공주 \"수아린\"을 구출하세요.";
            default:
                return "Rule of the dynasty Tang. Complete mission of the emperor. Rescue the princess \"Sua Lin\".";
        }
    }

    private string GetArcticMessage()
    {

        switch (Constants.language_current)
        {
            case "English":
                return "-35°C.... freezing... kill the Arctic mutant creatures and save your life.";
            case "Russian":
                return "- 35°C...замерзания...убей арктических существ - мутантов и спасти свою жизнь";
            case "Spanish":
                return "-35 °C.... helada... mata a las criaturas mutantes del Ártico y salva tu vida.";
            case "Italian":
                return "-35°C.... gelo... uccidi le creature mutanti artiche e salva la vita.";
            case "German":
                return "-35°C ... eiskalt ... töte die arktischen Mutantenkreaturen und rette dein Leben.";
            case "French":
                return "-35°C... congélation... tuez les créatures mutantes de l'Arctique et sauvez votre vie.";
            case "Portuguese":
                return "-35°C... congelando... mate as criaturas mutantes do Ártico e salve sua vida.";
            case "Japanese":
                return "-35°C ....凍結...北極圏の突然変異体の生き物を殺し、あなたの命を救ってください。";
            case "Chinese":
                return "-35°C.... 冰凍... 殺死北極變異生物並拯救您的生命。";
            case "Korean":
                return "-35°C.... 얼고... 북극 돌연변이 생물을 죽이고 생명을 구하십시오.";
            default:
                return "-35°C.... freezing... kill the Arctic mutant creatures and save your life.";
        }
    }

    private string GetForestMessage()
    {

        switch (Constants.language_current)
        {
            case "English":
                return "Rambo will not come to help you. So.. be against injustice as Rambo.";
            case "Russian":
                return "Рамбо не придёт тебе на помощь. Так что... будь против несправедливости, как Рамбо";
            case "Spanish":
                return "Rambo no vendrá a ayudarte. Así que... estar en contra de la injusticia como Rambo.";
            case "Italian":
                return "Rambo non verrà ad aiutarti. Quindi... essere contro l'ingiustizia come Rambo.";
            case "German":
                return "Rambo wird dir nicht helfen. Also ... als Rambo gegen Ungerechtigkeit sein.";
            case "French":
                return "Rambo ne viendra pas vous aider. Alors... soyez contre l'injustice comme Rambo.";
            case "Portuguese":
                return "Rambo não virá para ajudá-lo. Então... seja contra a injustiça como Rambo.";
            case "Japanese":
                return "ランボーはあなたを助けるために来ません。 だから..ランボーのように不当に反対しなさい。";
            case "Chinese":
                return "蘭博不會來幫你。 所以.. 像蘭博一樣反對不公正。";
            case "Korean":
                return "람보는 당신을 도우러 오지 않을 것입니다. 그러니.. 람보처럼 불의에 맞서십시오.";
            default:
                return "Rambo will not come to help you. So.. be against injustice as Rambo.";
        }
    }

    private string GetJurassicperiodMessage()
    {

        switch (Constants.language_current)
        {
            case "English":
                return "Travel to the Jurassic period and save yourself from danger.";
            case "Russian":
                return "Отправляйся в юрский период и спасти себя от опасности";
            case "Spanish":
                return "Viaja al período Jurásico y sálvate del peligro.";
            case "Italian":
                return "Viaggia nel periodo giurassico e salva te stesso dal pericolo.";
            case "German":
                return "Reise in die Jurazeit und rette dich vor Gefahren.";
            case "French":
                return "Voyagez au Jurassique et sauvez-vous du danger.";
            case "Portuguese":
                return "Viaje para o período jurássico e salve-se do perigo.";
            case "Japanese":
                return "ジュラ紀の時代に旅して、危険から身を守ってください。";
            case "Chinese":
                return "前往侏羅紀時期，將自己從危險中解救出來。";
            case "Korean":
                return "쥐라기 시대로 여행하고 위험으로부터 자신을 구하십시오.";
            default:
                return "Travel to the Jurassic period and save yourself from danger.";
        }
    }

    private string GetSafariMessage()
    {

        switch (Constants.language_current)
        {
            case "English":
                return "You are not at home. But you can be legend in here. Be as Lion King!";
            case "Russian":
                return "Ты не дома. Но ты можешь быть легендой здесь";
            case "Spanish":
                return "Tu no estas en casa. Pero puedes ser una leyenda aquí. ¡Sé como el Rey León!";
            case "Italian":
                return "Non sei a casa. Ma puoi essere una leggenda qui dentro. Sii come il Re Leone!";
            case "German":
                return "Du bist nicht zu hause. Aber Sie können hier eine Legende sein. Sei wie der König der Löwen!";
            case "French":
                return "Tu n'es pas à la maison. Mais vous pouvez être une légende ici. Soyez comme le Roi Lion!";
            case "Portuguese":
                return "Você não está em casa. Mas você pode ser lenda aqui. Seja como o Rei Leão!";
            case "Japanese":
                return "あなたは家にいません。 しかし、あなたはここで伝説になることができます。 ライオンキングになりましょう！";
            case "Chinese":
                return "你不在家。 但你可以在這裡成為傳奇。 成為獅子王！";
            case "Korean":
                return "당신은 집에 없습니다. 하지만 여기에서 당신은 전설이 될 수 있습니다. 라이온 킹이 되십시오!";
            default:
                return "You are not at home. But you can be legend in here. Be as Lion King!";
        }
    }


    private string GetVormirMessage()
    {

        switch (Constants.language_current)
        {
            case "English":
                return "You are Avenger, free the galaxy from Titan's attack.";
            case "Russian":
                return "Ты мститель, освободи галактику от атаки Титана.";
            case "Spanish":
                return "Eres Vengador, libera a la galaxia del ataque de Titán.";
            case "Italian":
                return "Tu sei Vendicatore, libera la galassia dall'attacco di Titano.";
            case "German":
                return "Du bist Avenger, befreie die Galaxie von Titans Angriff.";
            case "French":
                return "Vous êtes Avenger, libérez la galaxie de l'attaque de Titan.";
            case "Portuguese":
                return "Você é o Vingador, livre a galáxia do ataque de Titã.";
            case "Japanese":
                return "あなたは復讐者です、タイタンの攻撃から銀河を解放してください。";
            case "Chinese":
                return "你是複仇者，將銀河係從泰坦的攻擊中解放出來。";
            case "Korean":
                return "당신은 어벤저입니다, 타이탄의 공격으로부터 은하계를 해방하십시오.";
            default:
                return "You are Avenger, free the galaxy from Titan's attack.";
        }
    }

}
