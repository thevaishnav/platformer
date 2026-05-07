using System.Collections.Generic;
using UnityEngine;

public static class GameOverPhrases
{
    private static string[] _p0To10 = new[]
    {
        "Already?",
        "That Was Fast.",
        "Blink And You'll Miss It.",
        "Did You Even Try?",
        "New Record. Worst One.",
        "Speedrun. Wrong Kind.",
        "You Spawned. You Died.",
        "The Level Said No.",
        "Impressive. Not In A Good Way.",
        "Your Enemies Are Laughing.",
        "You Lasted Longer In The Tutorial.",
        "The Floor Was Not Lava. And Yet.",
        "A Valiant Zero Effort.",
        "Legend Has It You Tried.",
        "This Is Embarrassing For Both Of Us.",
        "Did Your Controller Unplug Itself?",
        "Even The Enemies Felt Bad.",
        "A New Low. Literally.",
        "History Will Not Remember This.",
        "You Peaked At The Title Screen."
    };
    
    private static string[] _p11To30 = new[]
    {
        "Getting Warmed Up?",
        "Bold Start. Terrible Finish.",
        "You Saw The Level. Then Left.",
        "Partial Credit: None.",
        "The Game Barely Noticed You.",
        "A Promising Beginning. A Tragic Middle.",
        "You Had Momentum. Briefly.",
        "The Enemies Are Still Confused.",
        "More Of A Visit Than A Run.",
        "You Found Out The Hard Way.",
        "It Gets Harder. You Got Softer.",
        "A Noble Attempt. Questionable Execution.",
        "Not Dead Last. Just Very Near It.",
        "The Level Shrugged.",
        "You Explored 30% Of Failure.",
        "Nice Warmup. Wrong Game.",
        "You Had Us Worried For A Second. We're Fine.",
        "Respectable Effort. Unrespectable Result.",
        "Practice Run? Sure, Let's Call It That.",
        "You Showed Up. Points For That. Not Really."
    };
    
    private static string[] _p31To50 = new[]
    {
        "Halfway Was Right There.",
        "Statistically, A Coin Flip Would've Done Better.",
        "You Made It To The Middle Of Nowhere.",
        "Half A Hero.",
        "The First Half Liked You. The Second Didn't.",
        "Peak Performance: Mediocrity.",
        "You Were Average. Then You Weren't.",
        "The Tipping Point Tipped.",
        "You Ran Out Of Steam At Half Steam.",
        "50% Courage. 50% Catastrophe.",
        "You Found The Middle. And Stayed There.",
        "Not Great, Not Terrible. Just Gone.",
        "A Journey To Nowhere In Particular.",
        "The Midpoint Is Laughing At You.",
        "Glass Half Empty. Glass Also Broken.",
        "You Crossed The Line. Wrong One.",
        "Solid Work On The Dying Part.",
        "Almost Inspiring. Almost.",
        "You Survived Long Enough To Really Disappoint.",
        "The Level Is Still Going. You Aren't."
    };
    
    private static string[] _p51To75 = new[]
    {
        "You were so close   (not really)",
        "The Finish Line Saw You Coming.",
        "You Were Good. Were.",
        "Tantalizingly Close. Painfully Not.",
        "A Story Of Promise And Peril.",
        "You Had A Great Run. Past Tense.",
        "The End Watched You Fall.",
        "You Tasted Victory. It Tasted Like Defeat.",
        "The Final Quarter Disagreed With You.",
        "So Near And Yet So Nowhere.",
        "You Outran Your Skill Level.",
        "A Hero's Journey. Minus The Hero Part.",
        "Impressive Distance. Disappointing Destination.",
        "You Fought Hard To Fail Gloriously.",
        "The Level Gave You A Chance. You Gave It Back.",
        "Destiny Was Within Reach. Then Wasn't.",
        "You Proved You Can Almost Do It.",
        "A Beautiful Disaster In Three Acts.",
        "The Second Half Is Not Your Friend.",
        "You Went Far Enough To Hurt."
    };
    
    private static string[] _p76To90 = new[]
    {
        "Almost There. (We Mean It This Time.)",
        "The Finish Line Moved. It Didn't, But Still.",
        "You Were This Close. This Is Not Close Enough.",
        "Victory Waved. You Tripped.",
        "The Last Stretch Stretches On Without You.",
        "Heartbreakingly Near.",
        "A Photo Finish. Minus The Photo. And Finish.",
        "You Scared The Level. It Scared Back.",
        "So Much Promise. Such A Specific Failure.",
        "Almost Counts In Horseshoes. Not Here.",
        "The End Was Watching. It Won.",
        "You Touched Glory. Glory Pulled Away.",
        "A Triumph Of Everything Except Completion.",
        "The Final Boss Knew You Were Coming.",
        "Aggressively, Stubbornly Almost.",
        "You Ran The Race. Forgot To Cross The Line.",
        "The Exit Is Right There. You Are Not.",
        "Agonizingly Short Of Legendary.",
        "History's Greatest Near-Miss.",
        "Next Time, The Universe Owes You One."
    };
    
    private static string[] _p90To100 = new[]
    {
        "Almost There.",
        "One More Step. You Needed One More Step.",
        "The Cruelest Kind Of Almost.",
        "You Breathed On The Finish Line.",
        "Technically Unfinished.",
        "The Level Blinked. So Did You.",
        "The Goal Line Laughed. We Didn't. (We Did.)",
        "You Made It. Just Not All The Way.",
        "A Masterclass In Being This Close.",
        "Devastating. Genuinely Devastating.",
        "The Credits Were Queued. They'll Wait.",
        "You Did Everything Right. Except Stop Dying.",
        "So Close The Win Screen Loaded Briefly.",
        "A Legend In Almost.",
        "The Final Pixel Defeated You.",
        "You Deserved It. It Disagreed.",
        "Inches From Immortality.",
        "The Level Shook Your Hand. Then Took It Back.",
        "A Perfect Run. Imperfectly Finished.",
        "You'll Feel This One Tomorrow."
    };
    
    public static string GetPhrase(int score)
    {
        if (score <= 10) return _p0To10[Random.Range(0, _p0To10.Length)];
        if (score <= 30) return _p11To30[Random.Range(0, _p11To30.Length)];
        if (score <= 50) return _p31To50[Random.Range(0, _p31To50.Length)];
        if (score <= 75) return _p51To75[Random.Range(0, _p51To75.Length)];
        if (score <= 90) return _p76To90[Random.Range(0, _p76To90.Length)];
        return _p90To100[Random.Range(0, _p90To100.Length)];
    }
}