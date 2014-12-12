<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="episodes.aspx.cs" Inherits="CSWeb.Tablet_COMPLETE.episodes" EnableSessionState="True" %>

<%@ Register Src="UserControls/TrackingPixels.ascx" TagPrefix="uc1" TagName="TrackingPixels" %>


<!doctype html>
<html>
<head runat="server">
<meta charset="utf-8"><meta name="viewport" content="width=device-width, maximum-scale=1.0">
<title>Batman | Classic TV Series Episodes Available on DVD and Bluray | As Seen on TV - Select Your Collection</title>
<meta name="description" content="Batman Classic TV Series - Now available on DVD & Bluray - Limited Time Offer!" />
<meta name="keywords" content="Batman Classic TV Series, DVD, Bluray, Warner Home Video, Adam West, As Seen on TV, Limited Time Offer, Robin, Joker, Riddler, Penguin, Catwoman, Original TV Series" />
<script src="//cdn.optimizely.com/js/77045885.js"></script>
<script src="//ajax.googleapis.com/ajax/libs/jquery/1.10.1/jquery.min.js"></script>
<link rel="stylesheet" type="text/css" href="/Scripts/fancybox/jquery.fancybox.css">
<script src="/Scripts/fancybox/jquery.fancybox.pack.js"></script>
<script type="text/javascript" src="/scripts/jwplayer/jwplayer.js"></script>
<script src="/Scripts/jquery.cycle.js"></script>
<script type="text/javascript">jwplayer.key = "JEtVDryJGkO9Q215yroU+Wz4oLeTJGMccGU/Wb3Kv9s=";</script>
<script src="/Scripts/global.js"></script>
<link href="/styles/global_big2complete_tablet.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
<!--#include file="popups.html"-->
<!--#include file="header.html"-->

<div class="container">
    <div class="episode_top">
        <h2 class="f49 pad20" style="margin-left: 14px;">Same Bat-Time...<br />
            <span class="block" style="padding-left: 15px;">...Same Bat-Channel!</span>
        </h2>
        <h3 class="f34 green caps webfont1bold">Select your Batman Collection:</h3>
        <select id="selectField" class="select-1" style="margin-left: 112px;">
            <option value="classic_collection">Classic Collection</option>
            <option value="complete_collection">Tablet_COMPLETE Collection</option>
        </select>
    </div>
    
    
    <div class="clear" style="height: 34px;"></div>


    <div id="classic_collection" class="episodebox">
        <div id="season1">
            <div class="clearfix bg_thwack">
                <div class="season_a">
                    <div class="season_hdr">
                        <h3>
                            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/episodes/eg_s1_disc1.jpg" alt="Season 1, Disc 1: Episodes 1-8" class="block" /></h3>
                    </div>
                    <div class="season_txt">
                        <ol class="episode_details">
                            <li><strong class="episode_title">Hi Diddle Riddle</strong> <span class="episode_date">(Original Airdate: January 12, 1966)</span><br />
                                In the series debut, the Prince of Puzzlers (Frank Gorshin as The Riddler) diabolically tricks the Dynamic Duo in order to sue them for false arrest, have them unmasked in court, and thus reveal their true identities.
                            </li>
                            <li><strong class="episode_title">Smack In The Middle</strong> <span class="episode_date">(Original Airdate: January 13, 1966)</span><br />
                                With Batman incapacitated by Molly, and Robin kidnapped by Riddler, it's no laughing matter when the heist of priceless postage stamps is Riddler's real play for his getaway. 
                            </li>
                            <li><strong class="episode_title">Fine Feathered Finks</strong> <span class="episode_date">(Original Airdate: January 19, 1966)</span><br />
                                Veteran actor Burgess Meredith makes his debut as one of Batman's most treacherous foes, and fresh out of prison Penguin and his foul finks waste no time wreaking havoc and tricking our Caped Crusaders into helping them commit their crime.
                            </li>
                            <li><strong class="episode_title">The Penguin's A Jinx</strong> <span class="episode_date">(Original Airdate: January 20, 1966)</span><br />
                                We last left Batman facing an inflammatory end over Penguin's flapping, but can a clever escape and ransom decoy put Penguin and his birdbrains back on ice? 

                            </li>
                            <li><strong class="episode_title">The Joker Is Wild</strong> <span class="episode_date">(Original Airdate: January 26, 1966)</span><br />
                                Just when you thought Gotham was safe again, along comes Joker — the Clown Prince Of Crime (Cesar Romero as The Joker) — who fashions a devilish utility belt of his own to help Batman and Boy Wonder take a powder.
                            </li>
                            <li><strong class="episode_title">Batman Is Riled</strong> <span class="episode_date">(Original Airdate: January 27, 1966)</span><br />
                                The Joker isn't clowning around as he plots more mayhem around the christening of the S.S. Gotham, and our Dynamic Duo play a little possum to bring him to justice.
                            </li>
                            <li><strong class="episode_title">Instant Freeze</strong> <span class="episode_date">(Original Airdate: February 2, 1966)</span><br />
                                There's the chill of chicanery in the air as Mr. Freeze (George Sanders) eyes the ice — diamonds that is! With his flame-freeze gun, he turns Batman and Robin into living popsicles to escape capture.
                            </li>
                            <li><strong class="episode_title">Rats Like Cheese</strong> <span class="episode_date">(Original Airdate: February 3, 1966)</span><br />
                                Thankfully, Gotham's Hospital de-ices our Dynamic Duo, but things get frostier with Mr. Freeze when he kidnaps a star pitcher. Cooler heads must prevail, along with thermal underwear, to melt Mr. Freeze into submission.
                            </li>
                        </ol>
                    </div>
                </div>
                <div class="season_b">
                    <div class="season_hdr">
                        <h3>
                            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/episodes/eg_s1_disc2.jpg" alt="Season 1, Disc 2: Episodes 9-16" class="block" /></h3>
                    </div>
                    <div class="season_txt">
                        <ol class="episode_details" style="counter-reset: item 8;">
                            <li><strong class="episode_title">Zelda The Great</strong> <span class="episode_date">(Original Airdate: February 9, 1966)</span><br />
                                The art of escape is perfected in Zelda The Great (Ann Baxter) as an April Fools' heist leads to Aunt Harriet's kidnapping. Holy backfires! Batman and Robin have one hour to rescue her from boiling oil.
                            </li>
                            <li><strong class="episode_title">A Death Worse Than Fate</strong> <span class="episode_date">(Original Airdate: February 10, 1966)</span><br />
                                The Doom-Trap is set even as Aunt Harriet is safely returned for the ransom, and Zelda has a change of heart. It's the truth about bats that lure Batman and Robin into an inescapable fate.
                            </li>
                            <li><strong class="episode_title">A Riddle A Day Keeps The Riddler Away</strong> <span class="episode_date">(Original Airdate: February 16, 1966)</span><br />
                                Riddler returns with a vengeance and puzzles on top of puzzles as a visiting King Boris disappears. The Dynamic Duo smash into a bone-crunching bind as they give chase.
                            </li>
                            <li><strong class="episode_title">When The Rat's Away The Mice Will Play</strong> <span class="episode_date">(Original Airdate: February 17, 1966)</span><br />
                                Not so fast Riddler, you haven't beaten our heroes yet! After escaping the drive shaft debacle, Batman and Robin try out-riddling rivals and some Biff, Pow, and Zlopp to exact justice. 
                            </li>
                            <li><strong class="episode_title">The Thirteenth Hat</strong> <span class="episode_date">(Original Airdate: February 23, 1966)</span><br />
                                In this double plot to un-cowl Batman and kidnap the jury that convicted him, Mad Hatter (David Wayne) doffs his Super Instant Mesmerizer. Just as Batman smells a hat-rat, a statue-shattering skirmish ensues, leaving Batman plastered up. 
                            </li>
                            <li><strong class="episode_title">Batman Stands Pat</strong> <span class="episode_date">(Original Airdate: February 24, 1966)</span><br />
                                It's Batman vs. Hatter's maniacal machines once freed from plaster to crack this case. But at the hat factory showdown, Batman is cleverly captured and it's Robin that must prevail.
                            </li>
                            <li><strong class="episode_title">The Joker Goes To School</strong> <span class="episode_date">(Original Airdate: March 2, 1966)</span><br />
                                Joker returns with a pernicious plot aimed at luring high schoolers into easy living. Rigged vending and slot machines ensnare the pursuing Batman and Robin, who are now wired to receive 50,000 volts!
                            </li>
                            <li><strong class="episode_title">He Meets His Match, The Grisly Ghoul</strong> <span class="episode_date">(Original Airdate: March 3, 1966)</span><br />
                                A massive power failure allows Batman and Robin the chance to foil the Joker and his Bad Pennies, but it's a close call with more rigged devices and poisoned perfume in the mix.
                            </li>
                        </ol>
                    </div>
                </div>

            </div>

            <div class="clearfix bg_powie">
                <div class="season_a">
                    <div class="season_hdr">
                        <h3>
                            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/episodes/eg_s1_disc3.jpg" alt="Season 1, Disc 3: Episodes 17-24" class="block" /></h3>
                    </div>
                    <div class="season_txt">
                        <ol class="episode_details" style="counter-reset: item 16;">
                            <li><strong class="episode_title">True Or False Face</strong> <span class="episode_date">(Original Airdate: March 9, 1966)</span><br />
                                He's the devil in disguise and treachery appears to triumph as False Face (Malachi Throne) steals the Mergenberg Crown and the trap is set. Batman and Robin find themselves in a sticky situation facing a speeding train!
                            </li>
                            <li><strong class="episode_title">Holy Rat Race</strong> <span class="episode_date">(Original Airdate: March 10, 1966)</span><br />
                                Batman and Robin get back on track to foil False Face who intends to swap counterfeit bills for the real thing. A series of phony fronts and false disguises have our heroes in hot pursuit!
                            </li>
                            <li><strong class="episode_title">The Purr-fect Crime</strong> <span class="episode_date">(Original Airdate: March 16, 1966)</span><br />
                                The felonious feline, Catwoman (Julie Newmar), gets her claws on twin Golden Cat statuettes from a Gotham museum, but our heroes fearlessly pursue their culprit right into a cat-trap, facing a ferocious tiger!
                            </li>
                            <li><strong class="episode_title">Better Luck Next Time</strong> <span class="episode_date">(Original Airdate: March 17, 1966)</span><br />
                                The Dynamic Duo is nearly devoured by ravenous lions and tigers as they race to capture Catwoman with her real target, the Captain Manx treasure trove!
                            </li>
                            <li><strong class="episode_title">The Penguin Goes Straight</strong> <span class="episode_date">(Original Airdate: March 23, 1966)</span><br />
                                What's this…Penguin giving up his life of crime with his "Protection" Agency? Sounds like a foul-feathered plot aimed at putting our Dynamic Duo on the ropes. Holy bulls-eye! 
                            </li>
                            <li><strong class="episode_title">Not Yet, He Ain't</strong> <span class="episode_date">(Original Airdate: March 24, 1966)</span><br />
                                Batboot soles and the Bat-claw-knife-blade come in very handy, as Batman and Robin set out to restore their good name…by acting insane? Well the chase is on and Bad Bird is going down! 
                            </li>
                            <li><strong class="episode_title">The Ring Of Wax</strong> <span class="episode_date">(Original Airdate: March 30, 1966)</span><br />
                                Riddler is back vexing authorities to get at the Lost Treasure Of The Incas. In pursuit our Dynamic Duo are duped, doped, and left dangling over boiling wax for a fatal double dip! 
                            </li>
                            <li><strong class="episode_title">Give 'Em The Axe</strong> <span class="episode_date">(Original Airdate: March 31, 1966)</span><br />
                                Before they are waxed, Batman and Robin blast free and news of their demise is too soon. Riddler's robbery must be derailed at the museum, his crummy crew captured amid medieval torture machines. 
                            </li>
                        </ol>
                    </div>
                </div>
                <div class="season_b">
                    <div class="season_hdr">
                        <h3>
                            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/episodes/eg_s1_disc4.jpg" alt="Season 1, Disc 4: Episodes 25-32" class="block" /></h3>
                    </div>
                    <div class="season_txt">
                        <ol class="episode_details" style="counter-reset: item 24;">
                            <li><strong class="episode_title">The Joker Trumps An Ace</strong> <span class="episode_date">(Original Airdate: April 6, 1966)</span><br />
                                The Maharajah's solid gold golf clubs prove irresistible to The Joker, but in a mix-up the Dynamic Duo must rescue his Highness too. What will become of our heroes tied up in a chimney with lethal gas? 
                            </li>
                            <li><strong class="episode_title">Batman Sets The Pace</strong> <span class="episode_date">(Original Airdate: April 7, 1966)</span><br />
                                To avoid an explosive international incident and ransom demand, Batman meets the Maharajah at the bank. He pounces on his Highness only to discover a Maharajah masquerade! 
                            </li>
                            <li><strong class="episode_title">The Curse Of Tut</strong> <span class="episode_date">(Original Airdate: April 13, 1966)</span><br />
                                An ex-Yale scholar believes himself to be the Great King, claiming Gotham as his Kingdom by planting a Sphinx in the park. Bruce Wayne plans to trap Tut (Victor Buono), but before you can say "mummy," he's a fall guy!
                            </li>
                            <li><strong class="episode_title">The Pharaoh's In A Rut</strong> <span class="episode_date">(Original Airdate: April 14, 1966)</span><br />
                                Tut now turns to ancient Theban pebble torture on a kidnapped Batman to make him a mindless slave. Robin must come to his aid, and the chase is on to short circuit the torturous Tut.
                            </li>
                            <li><strong class="episode_title">The Bookworm Turns</strong> <span class="episode_date">(Original Airdate: April 20, 1966)</span><br />
                                Bookworm (Roddy McDowall), the villain of volumes, draws the Dynamic Duo into an explosive ruse and false clues. Robin is left strapped to the clapper of the Big Benjamin bell, with a minute to toll!
                            </li>
                            <li><strong class="episode_title">While Gotham City Burns</strong> <span class="episode_date">(Original Airdate: April 21, 1966)</span><br />
                                It's a recipe for disaster as Bookworm diabolically designs a giant cookbook caper. Our crusaders against crime are getting steamed as Bookworm heads off in the Batmobile for the riches of Morganbilt Library!
                            </li>
                            <li><strong class="episode_title">Death In Slow Motion</strong> <span class="episode_date">(Original Airdate: April 27, 1966)</span><br />
                                Riddler directs silent movie mayhem as he steals box office receipts and payrolls while staging his capers as film productions. The spree separates the Dynamic Duo and lines Robin up with a buzz saw. Holy kindling!
                            </li>
                            <li><strong class="episode_title">The Riddler's False Notion</strong> <span class="episode_date">(Original Airdate: April 28, 1966)</span><br />
                                Riddled with clues, Batman speeds to Robin's rescue, but discovers his real fate is dropping from a high building a la Harold Lloyd silent movies. Riddler's egregious greed lands him top billing in "Up The River!"
                            </li>
                        </ol>
                    </div>
                </div>

            </div>

            <div class="clearfix">
                <div class="season_a">
                    <div class="season_hdr">
                        <h3>
                            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/episodes/eg_s1_disc5.jpg" alt="Season 1, Disc 5: Episodes 33-34" class="block" /></h3>
                    </div>
                    <div class="season_txt">
                        <ol class="episode_details" style="counter-reset: item 32;">
                            <li><strong class="episode_title">Fine Finny Fiends</strong> <span class="episode_date">(Original Airdate: May 4, 1966)</span><br />
                                Alfred becomes an unwilling victim of Penguin's bird brainwash, revealing secrets about Bruce Wayne's Multimillionaires' Annual Award Dinner. An umbrella trap set, our Dynamic Duo is caught gasping for air. 

                            </li>
                            <li style="padding-bottom: 4px;"><strong class="episode_title">Batman Makes The Scenes</strong> <span class="episode_date">(Original Airdate: May 5, 1966)</span><br />
                                The Dynamic Duo is left for dead, but not so fast. Penguin thinks he'll just fly off with the loot meant for charity at the Annual Dinner, but this bad bird is about to find himself all tied up in a net!
                            </li>
                        </ol>
                    </div>
                </div>
                <div class="season_b">
                </div>

            </div>


            <%--<div class="episode_extra">
            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/disc5set1.png" alt="" class="block" style="position: absolute; top: -76px; left: 21px; z-index: 20;" />
            <div class="clearfix">
                <div class="season_a">&nbsp;</div>
                <div class="season_b">
                    <div class="season_txt" style="padding-top: 24px; padding-bottom: 0;">
                    </div>
                </div>
            </div>
        </div>--%>
        </div>
        <!-- END season1 -->



        <div id="season2">
            <div class="clearfix bg_thwack">
                <div class="season_a">
                    <div class="season_hdr">
                        <h3>
                            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/episodes/eg_s2_disc6.jpg" alt="Season 2, Disc 6: Episodes 34-42" class="block" /></h3>
                    </div>
                    <div class="season_txt">
                        <ol class="episode_details" style="counter-reset: item 34;">
                            <li><strong class="episode_title">Shoot A Crooked Arrow</strong> <span class="episode_date">(Original Airdate: September 7, 1966)</span><br />
                                The Archer (Art Carney) manages to loot Wayne Manor with his band of merry malefactors, flaunt justice and become a hero to Gotham's poor. Aiming for the location of the Batcave, Archer sets up Batman and Robin to make his point! 
                            </li>
                            <li><strong class="episode_title">Walk The Straight And Narrow</strong> <span class="episode_date">(Original Airdate: September 8, 1966)</span><br />
                                Our heroes spring back into action, but Archer is using arrows that fly around corners to nab Bruce Wayne's $10,000,000 earmarked for charity. All might be lost if not for quick thinking and the Batboat.  
                            </li>
                            <li><strong class="episode_title">Hot Off The Griddle</strong> <span class="episode_date">(Original Airdate: September 14, 1966)</span><br />
                                Catwoman is back on the prowl, slinking for prizes to purloin and equipped with cat-darts filled with Catatonic for our dauntless duo. Its affects leave Batman and Robin hotfooting and broiling mad.
                            </li>
                            <li><strong class="episode_title">The Cat And The Fiddle</strong> <span class="episode_date">(Original Airdate: September 15, 1966)</span><br />
                                When Catwoman pounces on priceless violins in a shameless penthouse ruse, Robin and Batman team to foil the feline. At dizzying heights the chase is on, in and out of windows, and it will take a miracle to capture this Cat!
                            </li>
                            <li><strong class="episode_title">The Minstrel's Shakedown</strong> <span class="episode_date">(Original Airdate: September 21, 1966)</span><br />
                                There's a new threat in Gotham, the electronic genius and talented lute player Minstrel (Van Johnson). His devious plot is to sabotage the Stock Exchange. When Batman and Robin close his circuits, the heat is on.
                            </li>
                            <li><strong class="episode_title">Barbecued Batman?</strong> <span class="episode_date">(Original Airdate: September 22, 1966)</span><br />
                                Minstrel fine-tunes his evil efforts to extort, instituting operation Low "C." His final threat doesn't resonate with the Dynamic Duo, but Minstrel is on a real power trip.
                            </li>
                            <li><strong class="episode_title">The Spell Of Tut</strong> <span class="episode_date">(Original Airdate: September 28, 1966)</span><br />
                                In a sinister scheme, Tut pursues ancient scarabs in order to produce a potion that will sap human will. Batman and Robin have a plan to foil the Pharaoh, but Robin winds up walking an ever-shrinking plank over a pit of crocs!
                            </li>
                            <li><strong class="episode_title">Tut's Case Is Shut</strong> <span class="episode_date">(Original Airdate: September 29, 1966)</span><br />
                                King Tut deploys his injurious bug juice, slipping scarab mickeys to law enforcement! Our Peerless Pair must nail this nefarious no-good before the powerful potion gets in the water supply. Got buttermilk?
                            </li>
                        </ol>
                    </div>
                </div>
                <div class="season_b">
                    <div class="season_hdr">
                        <h3>
                            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/episodes/eg_s2_disc7.jpg" alt="Season 2, Disc 7: Episodes 43-50" class="block" /></h3>
                    </div>
                    <div class="season_txt">
                        <ol class="episode_details" style="counter-reset: item 42;">
                            <li><strong class="episode_title">The Greatest Mother Of Them All</strong> <span class="episode_date">(Original Airdate: October 5, 1966)</span><br />
                                When Ma Parker (Shelley Winters) and her corrupt kids go on a Gotham crime spree, Batman and Robin soundly round them up. It was all too easy, though, as Ma's gang takes over the prison and the Batmobile is rigged to become spare parts!
                            </li>
                            <li><strong class="episode_title">Ma Parker</strong> <span class="episode_date">(Original Airdate: October 6, 1966)</span><br />
                                Ma Parker and her criminal cohorts now have the perfect cover and base of operations. It will take all of Batman and Robin's deductive powers to crack this case, but Ma Parker is ready to sentence them to the electric chair.
                            </li>
                            <li><strong class="episode_title">The Clock King's Crazy Crimes</strong> <span class="episode_date">(Original Airdate: October 12, 1966)</span><br />
                                Clock King (Walter Slezak) and his Second Hands horde use an antique clock con to loot a jewelry store. Before you can say, "Holy timepiece!", the Dynamic Duo spring forward and fall prey to a giant hourglass and the sands of time!
                            </li>
                            <li><strong class="episode_title">The Clock King Gets Crowned</strong> <span class="episode_date">(Original Airdate: October 13, 1966)</span><br />
                                Clock King really wants to pilfer Bruce Wayne's priceless antique pocket watch collection, using an unwitting Aunt Harriet in the scheme. The clamorous clock caper unwinds into a race against time for Batman and Robin.
                            </li>
                            <li><strong class="episode_title">An Egg Grows In Gotham</strong> <span class="episode_date">(Original Airdate: October 19, 1966)</span><br />
                                An egg-ceptionally greedy Egghead (Vincent Price) and his lackeys are poised to poach Gotham's charter and use a loophole to own it all. The Dynamic Duo is onto him, but in a scathing scramble Egghead's Truth Machine could leave our heroes at wit's end.
                            </li>
                            <li><strong class="episode_title">The Yegg Foes In Gotham</strong> <span class="episode_date">(Original Airdate: October 20, 1966)</span><br />
                                Bruce and Dick's quick thinking closes Egghead's loophole, but he's not going to fry without a fight. He plans to plunder the City Treasury and run, but Batman and Robin are ready with a little bomb shelling.
                            </li>
                            <li><strong class="episode_title">The Devil's Fingers</strong> <span class="episode_date">(Original Airdate: October 26, 1966)</span><br />
                                While piano virtuoso, Chandell (Liberace), tickles the ivories for Aunt Harriet, a high-pitched heist hits Wayne Manor with Bruce and Dick away. The next concert strikes a false chord bringing the Dynamic Duo together, only to face the cutting room floor!
                            </li>
                            <li><strong class="episode_title">The Dead Ringers</strong> <span class="episode_date">(Original Airdate: October 27, 1966)</span><br />
                                Tablet_COMPLETE with imposters, dual identities and blackmail, this atrocious arrangement is full of sour notes and the Wayne fortune is in play. Batman and Robin have to get in tune quickly to make the maestros of mayhem face the music. 
                            </li>
                        </ol>
                    </div>
                </div>

            </div>

            <div class="clearfix bg_powie">
                <div class="season_a">
                    <div class="season_hdr">
                        <h3>
                            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/episodes/eg_s2_disc8.jpg" alt="Season 2, Disc 8: Episodes 51-58" class="block" /></h3>
                    </div>
                    <div class="season_txt">
                        <ol class="episode_details" style="counter-reset: item 50;">
                            <li><strong class="episode_title">Hizzonner The Penguin</strong> <span class="episode_date">(Original Airdate: November 2, 1966)</span><br />
                                Suddenly Penguin makes himself popular in the polls by endearing the citizens with good deeds in a beaky bid for Mayor.  Batman must throw his cowl into the ring, but the brash bird's campaign tactics may melt down the race.
                            </li>
                            <li><strong class="episode_title">Dizzoner The Penguin</strong> <span class="episode_date">(Original Airdate: November 3, 1966)</span><br />
                                Batman and Penguin continue their campaign debates and the feathers fly, but suddenly punks are pinching jewels at a convention. Penguin pops more crooks than Batman, and just might become Mayor! 
                            </li>
                            <li><strong class="episode_title">Green Ice</strong> <span class="episode_date">(Original Airdate: November 9, 1966)</span><br />
                                The malevolent Mr. Freeze (Otto Preminger) sends shivers throughout Gotham City with glacier of misdeeds all chipping away at Batman and Robin's reputation. Hot on his trial in the Batmobile, our Caped Crusaders are jumped and left to become frozen popsicles.
                            </li>
                            <li><strong class="episode_title">Deep Freeze</strong> <span class="episode_date">(Original Airdate: November 10, 1966)</span><br />
                                The cold crime wave continues with a vengeance as Freeze chills the air with threats to put all of Gotham on ice. Batman and Robin spring back into action to defrost felonious Freeze and his forces.
                            </li>
                            <li><strong class="episode_title">The Impractical Joker</strong> <span class="episode_date">(Original Airdate: November 16, 1966)</span><br />
                                The Joker just seems to want the keys to Gotham City handed to him as his latest crime spree sparks obscure clues. Meanwhile, back at the Batcave, the Dynamic Duo deciphers Joker's hideout only to be trapped lock, stock and spray wax!
                            </li>
                            <li><strong class="episode_title">The Joker's Provokers</strong> <span class="episode_date">(Original Airdate: November 17, 1966)</span><br />
                                Batman and Robin miraculously decode the true aim of our Clown Prince of Crime, but Joker has a time machine in a box that could play havoc. Using Alfred's help, our heroes go all out to put the fisticuffs on these felons.
                            </li>
                            <li><strong class="episode_title">Marsha, Queen Of Diamonds</strong> <span class="episode_date">(Original Airdate: November 23, 1966)</span><br />
                                Maleficent Marsha (Carolyn Jones) is as persuasive as she is power hungry. She's after the Batdiamond, which drives the Batcomputer at all costs. Once she has Robin under her spell, Batman must marry her to save him. Holy matrimony! A Dynamic Trio?
                            </li>
                            <li><strong class="episode_title">Marsha's Scheme Of Diamonds</strong> <span class="episode_date">(Original Airdate: November 24, 1966)</span><br />
                                Aunt Harriet and Alfred must save Batman from the altar, but the Queen Of Diamond will stop at nothing and seeks more powerful potions to pour. And horrors, it appears she's cut our Crusaders down to size — two caped toads!
                            </li>
                        </ol>
                    </div>
                </div>
                <div class="season_b">
                    <div class="season_hdr">
                        <h3>
                            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/episodes/eg_s2_disc9.jpg" alt="Season 2, Disc 9: Episodes 59-64" class="block" /></h3>
                    </div>
                    <div class="season_txt">
                        <ol class="episode_details" style="counter-reset: item 58;">
                            <li><strong class="episode_title">Come Back, Shame</strong> <span class="episode_date">(Original Airdate: November 30, 1966)</span><br />
                                Crusty cowpoke Shame (Cliff Robertson) is all over town stealing hot car parts so he can outrun the Batmobile with a fast truck. In a clever twist, Wayne's limo is used to snare the hombre, but Shame gets a drop on the Duo and they're staked out!
                            </li>
                            <li><strong class="episode_title">It's How You Play The Game</strong> <span class="episode_date">(Original Airdate: December 1, 1966)</span><br />
                                Shame's true aim is on four prize bulls worth over a million dollars, and he rustles them away easily in his super truck. Batman steers his attention to the K.O. Corral and feeding time, where he expects a final showdown.
                            </li>
                            <li><strong class="episode_title">The Penguin's Nest</strong> <span class="episode_date">(Original Airdate: December 7, 1966)</span><br />
                                Penguin opens a high-end eatery, but with a catch. Wealthy patrons write their own orders, providing the crafty con with a flock of samples. Now if he can get arrested, it's off to forge a perfect prison partnership!
                            </li>
                            <li><strong class="episode_title">The Bird's Last Jest</strong> <span class="episode_date">(Original Airdate: December 8, 1966)</span><br />
                                Penguin can't seem to get back to prison and partner with Ballpoint Baxter to forge checks, so the boisterous Bird-man and his ruffian restaurateurs serve up a desperate ransom — pie a la Alfred!
                            </li>
                            <li><strong class="episode_title">The Cat's Meow</strong> <span class="episode_date">(Original Airdate: December 14, 1966)</span><br />
                                Catwoman strays back into annoying action, and plans to steal the voices of rave English rockers. As she and her larcenous litter try to get near their target, Batman and Robin rush to foil the felines only to be subjected to echo chamber agony.
                            </li>
                            <li><strong class="episode_title">The Bat's Kow Tow</strong> <span class="episode_date">(Original Airdate: December 15, 1966)</span><br />
                                Sadly, Catwoman's Voice-Eraser works and she steals Chad and Jeremy's voices for a ravenous ransom. Batman and Robin trace where her calls are coming from, but Catwoman's lethal sonic gun stands in the way of justice.
                            </li>
                        </ol>
                    </div>
                </div>
            </div>


        </div>
        <!-- END season2 -->

        <!--#include file="bottomcta.html"-->
    </div>
    <!-- END CLASSIC COLLECTION -->









    <div id="complete_collection" class="episodebox">
        <div id="season1">
            <div class="clearfix bg_thwack">
                <div class="season_a">
                    <div class="season_hdr">
                        <h3>
                            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/episodes/eg_s1_disc1.jpg" alt="Season 1, Disc 1: Episodes 1-8" class="block" /></h3>
                    </div>
                    <div class="season_txt">
                        <ol class="episode_details">
                            <li><strong class="episode_title">Hi Diddle Riddle</strong> <span class="episode_date">(Original Airdate: January 12, 1966)</span><br />
                                In the series debut, the Prince of Puzzlers (Frank Gorshin as The Riddler) diabolically tricks the Dynamic Duo in order to sue them for false arrest, have them unmasked in court, and thus reveal their true identities.
                            </li>
                            <li><strong class="episode_title">Smack In The Middle</strong> <span class="episode_date">(Original Airdate: January 13, 1966)</span><br />
                                With Batman incapacitated by Molly, and Robin kidnapped by Riddler, it's no laughing matter when the heist of priceless postage stamps is Riddler's real play for his getaway. 
                            </li>
                            <li><strong class="episode_title">Fine Feathered Finks</strong> <span class="episode_date">(Original Airdate: January 19, 1966)</span><br />
                                Veteran actor Burgess Meredith makes his debut as one of Batman's most treacherous foes, and fresh out of prison Penguin and his foul finks waste no time wreaking havoc and tricking our Caped Crusaders into helping them commit their crime.
                            </li>
                            <li><strong class="episode_title">The Penguin's A Jinx</strong> <span class="episode_date">(Original Airdate: January 20, 1966)</span><br />
                                We last left Batman facing an inflammatory end over Penguin's flapping, but can a clever escape and ransom decoy put Penguin and his birdbrains back on ice? 

                            </li>
                            <li><strong class="episode_title">The Joker Is Wild</strong> <span class="episode_date">(Original Airdate: January 26, 1966)</span><br />
                                Just when you thought Gotham was safe again, along comes Joker — the Clown Prince Of Crime (Cesar Romero as The Joker) — who fashions a devilish utility belt of his own to help Batman and Boy Wonder take a powder.
                            </li>
                            <li><strong class="episode_title">Batman Is Riled</strong> <span class="episode_date">(Original Airdate: January 27, 1966)</span><br />
                                The Joker isn't clowning around as he plots more mayhem around the christening of the S.S. Gotham, and our Dynamic Duo play a little possum to bring him to justice.
                            </li>
                            <li><strong class="episode_title">Instant Freeze</strong> <span class="episode_date">(Original Airdate: February 2, 1966)</span><br />
                                There's the chill of chicanery in the air as Mr. Freeze (George Sanders) eyes the ice — diamonds that is! With his flame-freeze gun, he turns Batman and Robin into living popsicles to escape capture.
                            </li>
                            <li><strong class="episode_title">Rats Like Cheese</strong> <span class="episode_date">(Original Airdate: February 3, 1966)</span><br />
                                Thankfully, Gotham's Hospital de-ices our Dynamic Duo, but things get frostier with Mr. Freeze when he kidnaps a star pitcher. Cooler heads must prevail, along with thermal underwear, to melt Mr. Freeze into submission.
                            </li>
                        </ol>
                    </div>
                </div>
                <div class="season_b">
                    <div class="season_hdr">
                        <h3>
                            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/episodes/eg_s1_disc2.jpg" alt="Season 1, Disc 2: Episodes 9-16" class="block" /></h3>
                    </div>
                    <div class="season_txt">
                        <ol class="episode_details" style="counter-reset: item 8;">
                            <li><strong class="episode_title">Zelda The Great</strong> <span class="episode_date">(Original Airdate: February 9, 1966)</span><br />
                                The art of escape is perfected in Zelda The Great (Ann Baxter) as an April Fools' heist leads to Aunt Harriet's kidnapping. Holy backfires! Batman and Robin have one hour to rescue her from boiling oil.
                            </li>
                            <li><strong class="episode_title">A Death Worse Than Fate</strong> <span class="episode_date">(Original Airdate: February 10, 1966)</span><br />
                                The Doom-Trap is set even as Aunt Harriet is safely returned for the ransom, and Zelda has a change of heart. It's the truth about bats that lure Batman and Robin into an inescapable fate.
                            </li>
                            <li><strong class="episode_title">A Riddle A Day Keeps The Riddler Away</strong> <span class="episode_date">(Original Airdate: February 16, 1966)</span><br />
                                Riddler returns with a vengeance and puzzles on top of puzzles as a visiting King Boris disappears. The Dynamic Duo smash into a bone-crunching bind as they give chase.
                            </li>
                            <li><strong class="episode_title">When The Rat's Away The Mice Will Play</strong> <span class="episode_date">(Original Airdate: February 17, 1966)</span><br />
                                Not so fast Riddler, you haven't beaten our heroes yet! After escaping the drive shaft debacle, Batman and Robin try out-riddling rivals and some Biff, Pow, and Zlopp to exact justice. 
                            </li>
                            <li><strong class="episode_title">The Thirteenth Hat</strong> <span class="episode_date">(Original Airdate: February 23, 1966)</span><br />
                                In this double plot to un-cowl Batman and kidnap the jury that convicted him, Mad Hatter (David Wayne) doffs his Super Instant Mesmerizer. Just as Batman smells a hat-rat, a statue-shattering skirmish ensues, leaving Batman plastered up. 
                            </li>
                            <li><strong class="episode_title">Batman Stands Pat</strong> <span class="episode_date">(Original Airdate: February 24, 1966)</span><br />
                                It's Batman vs. Hatter's maniacal machines once freed from plaster to crack this case. But at the hat factory showdown, Batman is cleverly captured and it's Robin that must prevail.
                            </li>
                            <li><strong class="episode_title">The Joker Goes To School</strong> <span class="episode_date">(Original Airdate: March 2, 1966)</span><br />
                                Joker returns with a pernicious plot aimed at luring high schoolers into easy living. Rigged vending and slot machines ensnare the pursuing Batman and Robin, who are now wired to receive 50,000 volts!
                            </li>
                            <li><strong class="episode_title">He Meets His Match, The Grisly Ghoul</strong> <span class="episode_date">(Original Airdate: March 3, 1966)</span><br />
                                A massive power failure allows Batman and Robin the chance to foil the Joker and his Bad Pennies, but it's a close call with more rigged devices and poisoned perfume in the mix.
                            </li>
                        </ol>
                    </div>
                </div>

            </div>

            <div class="clearfix bg_powie">
                <div class="season_a">
                    <div class="season_hdr">
                        <h3>
                            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/episodes/eg_s1_disc3.jpg" alt="Season 1, Disc 3: Episodes 17-24" class="block" /></h3>
                    </div>
                    <div class="season_txt">
                        <ol class="episode_details" style="counter-reset: item 16;">
                            <li><strong class="episode_title">True Or False Face</strong> <span class="episode_date">(Original Airdate: March 9, 1966)</span><br />
                                He's the devil in disguise and treachery appears to triumph as False Face (Malachi Throne) steals the Mergenberg Crown and the trap is set. Batman and Robin find themselves in a sticky situation facing a speeding train!
                            </li>
                            <li><strong class="episode_title">Holy Rat Race</strong> <span class="episode_date">(Original Airdate: March 10, 1966)</span><br />
                                Batman and Robin get back on track to foil False Face who intends to swap counterfeit bills for the real thing. A series of phony fronts and false disguises have our heroes in hot pursuit!
                            </li>
                            <li><strong class="episode_title">The Purr-fect Crime</strong> <span class="episode_date">(Original Airdate: March 16, 1966)</span><br />
                                The felonious feline, Catwoman (Julie Newmar), gets her claws on twin Golden Cat statuettes from a Gotham museum, but our heroes fearlessly pursue their culprit right into a cat-trap, facing a ferocious tiger!
                            </li>
                            <li><strong class="episode_title">Better Luck Next Time</strong> <span class="episode_date">(Original Airdate: March 17, 1966)</span><br />
                                The Dynamic Duo is nearly devoured by ravenous lions and tigers as they race to capture Catwoman with her real target, the Captain Manx treasure trove!
                            </li>
                            <li><strong class="episode_title">The Penguin Goes Straight</strong> <span class="episode_date">(Original Airdate: March 23, 1966)</span><br />
                                What's this…Penguin giving up his life of crime with his "Protection" Agency? Sounds like a foul-feathered plot aimed at putting our Dynamic Duo on the ropes. Holy bulls-eye! 
                            </li>
                            <li><strong class="episode_title">Not Yet, He Ain't</strong> <span class="episode_date">(Original Airdate: March 24, 1966)</span><br />
                                Batboot soles and the Bat-claw-knife-blade come in very handy, as Batman and Robin set out to restore their good name…by acting insane? Well the chase is on and Bad Bird is going down! 
                            </li>
                            <li><strong class="episode_title">The Ring Of Wax</strong> <span class="episode_date">(Original Airdate: March 30, 1966)</span><br />
                                Riddler is back vexing authorities to get at the Lost Treasure Of The Incas. In pursuit our Dynamic Duo are duped, doped, and left dangling over boiling wax for a fatal double dip! 
                            </li>
                            <li><strong class="episode_title">Give 'Em The Axe</strong> <span class="episode_date">(Original Airdate: March 31, 1966)</span><br />
                                Before they are waxed, Batman and Robin blast free and news of their demise is too soon. Riddler's robbery must be derailed at the museum, his crummy crew captured amid medieval torture machines. 
                            </li>
                        </ol>
                    </div>
                </div>
                <div class="season_b">
                    <div class="season_hdr">
                        <h3>
                            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/episodes/eg_s1_disc4.jpg" alt="Season 1, Disc 4: Episodes 25-32" class="block" /></h3>
                    </div>
                    <div class="season_txt">
                        <ol class="episode_details" style="counter-reset: item 24;">
                            <li><strong class="episode_title">The Joker Trumps An Ace</strong> <span class="episode_date">(Original Airdate: April 6, 1966)</span><br />
                                The Maharajah's solid gold golf clubs prove irresistible to The Joker, but in a mix-up the Dynamic Duo must rescue his Highness too. What will become of our heroes tied up in a chimney with lethal gas? 
                            </li>
                            <li><strong class="episode_title">Batman Sets The Pace</strong> <span class="episode_date">(Original Airdate: April 7, 1966)</span><br />
                                To avoid an explosive international incident and ransom demand, Batman meets the Maharajah at the bank. He pounces on his Highness only to discover a Maharajah masquerade! 
                            </li>
                            <li><strong class="episode_title">The Curse Of Tut</strong> <span class="episode_date">(Original Airdate: April 13, 1966)</span><br />
                                An ex-Yale scholar believes himself to be the Great King, claiming Gotham as his Kingdom by planting a Sphinx in the park. Bruce Wayne plans to trap Tut (Victor Buono), but before you can say "mummy," he's a fall guy!
                            </li>
                            <li><strong class="episode_title">The Pharaoh's In A Rut</strong> <span class="episode_date">(Original Airdate: April 14, 1966)</span><br />
                                Tut now turns to ancient Theban pebble torture on a kidnapped Batman to make him a mindless slave. Robin must come to his aid, and the chase is on to short circuit the torturous Tut.
                            </li>
                            <li><strong class="episode_title">The Bookworm Turns</strong> <span class="episode_date">(Original Airdate: April 20, 1966)</span><br />
                                Bookworm (Roddy McDowall), the villain of volumes, draws the Dynamic Duo into an explosive ruse and false clues. Robin is left strapped to the clapper of the Big Benjamin bell, with a minute to toll!
                            </li>
                            <li><strong class="episode_title">While Gotham City Burns</strong> <span class="episode_date">(Original Airdate: April 21, 1966)</span><br />
                                It's a recipe for disaster as Bookworm diabolically designs a giant cookbook caper. Our crusaders against crime are getting steamed as Bookworm heads off in the Batmobile for the riches of Morganbilt Library!
                            </li>
                            <li><strong class="episode_title">Death In Slow Motion</strong> <span class="episode_date">(Original Airdate: April 27, 1966)</span><br />
                                Riddler directs silent movie mayhem as he steals box office receipts and payrolls while staging his capers as film productions. The spree separates the Dynamic Duo and lines Robin up with a buzz saw. Holy kindling!
                            </li>
                            <li><strong class="episode_title">The Riddler's False Notion</strong> <span class="episode_date">(Original Airdate: April 28, 1966)</span><br />
                                Riddled with clues, Batman speeds to Robin's rescue, but discovers his real fate is dropping from a high building a la Harold Lloyd silent movies. Riddler's egregious greed lands him top billing in "Up The River!"
                            </li>
                        </ol>
                    </div>
                </div>

            </div>

            <div class="clearfix">
                <div class="season_a">
                    <div class="season_hdr">
                        <h3>
                            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/episodes/eg_s1_disc5.jpg" alt="Season 1, Disc 5: Episodes 33-34" class="block" /></h3>
                    </div>
                    <div class="season_txt">
                        <ol class="episode_details" style="counter-reset: item 32;">
                            <li><strong class="episode_title">Fine Finny Fiends</strong> <span class="episode_date">(Original Airdate: May 4, 1966)</span><br />
                                Alfred becomes an unwilling victim of Penguin's bird brainwash, revealing secrets about Bruce Wayne's Multimillionaires' Annual Award Dinner. An umbrella trap set, our Dynamic Duo is caught gasping for air. 

                            </li>
                            <li style="padding-bottom: 4px;"><strong class="episode_title">Batman Makes The Scenes</strong> <span class="episode_date">(Original Airdate: May 5, 1966)</span><br />
                                The Dynamic Duo is left for dead, but not so fast. Penguin thinks he'll just fly off with the loot meant for charity at the Annual Dinner, but this bad bird is about to find himself all tied up in a net!
                            </li>
                        </ol>
                    </div>
                </div>
                <div class="season_b">
                </div>

            </div>


            <%--<div class="episode_extra">
            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/disc5set1.png" alt="" class="block" style="position: absolute; top: -76px; left: 21px; z-index: 20;" />
            <div class="clearfix">
                <div class="season_a">&nbsp;</div>
                <div class="season_b">
                    <div class="season_txt" style="padding-top: 24px; padding-bottom: 0;">
                    </div>
                </div>
            </div>
        </div>--%>
        </div>
        <!-- END season1 -->




        <div id="season2">
            <div class="clearfix bg_thwack">
                <div class="season_a">
                    <div class="season_hdr">
                        <h3>
                            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/episodes/eg_s2_disc6.jpg" alt="Season 2, Disc 6: Episodes 34-42" class="block" /></h3>
                    </div>
                    <div class="season_txt">
                        <ol class="episode_details" style="counter-reset: item 34;">
                            <li><strong class="episode_title">Shoot A Crooked Arrow</strong> <span class="episode_date">(Original Airdate: September 7, 1966)</span><br />
                                The Archer (Art Carney) manages to loot Wayne Manor with his band of merry malefactors, flaunt justice and become a hero to Gotham's poor. Aiming for the location of the Batcave, Archer sets up Batman and Robin to make his point! 
                            </li>
                            <li><strong class="episode_title">Walk The Straight And Narrow</strong> <span class="episode_date">(Original Airdate: September 8, 1966)</span><br />
                                Our heroes spring back into action, but Archer is using arrows that fly around corners to nab Bruce Wayne's $10,000,000 earmarked for charity. All might be lost if not for quick thinking and the Batboat.  
                            </li>
                            <li><strong class="episode_title">Hot Off The Griddle</strong> <span class="episode_date">(Original Airdate: September 14, 1966)</span><br />
                                Catwoman is back on the prowl, slinking for prizes to purloin and equipped with cat-darts filled with Catatonic for our dauntless duo. Its affects leave Batman and Robin hotfooting and broiling mad.
                            </li>
                            <li><strong class="episode_title">The Cat And The Fiddle</strong> <span class="episode_date">(Original Airdate: September 15, 1966)</span><br />
                                When Catwoman pounces on priceless violins in a shameless penthouse ruse, Robin and Batman team to foil the feline. At dizzying heights the chase is on, in and out of windows, and it will take a miracle to capture this Cat!
                            </li>
                            <li><strong class="episode_title">The Minstrel's Shakedown</strong> <span class="episode_date">(Original Airdate: September 21, 1966)</span><br />
                                There's a new threat in Gotham, the electronic genius and talented lute player Minstrel (Van Johnson). His devious plot is to sabotage the Stock Exchange. When Batman and Robin close his circuits, the heat is on.
                            </li>
                            <li><strong class="episode_title">Barbecued Batman?</strong> <span class="episode_date">(Original Airdate: September 22, 1966)</span><br />
                                Minstrel fine-tunes his evil efforts to extort, instituting operation Low "C." His final threat doesn't resonate with the Dynamic Duo, but Minstrel is on a real power trip.
                            </li>
                            <li><strong class="episode_title">The Spell Of Tut</strong> <span class="episode_date">(Original Airdate: September 28, 1966)</span><br />
                                In a sinister scheme, Tut pursues ancient scarabs in order to produce a potion that will sap human will. Batman and Robin have a plan to foil the Pharaoh, but Robin winds up walking an ever-shrinking plank over a pit of crocs!
                            </li>
                            <li><strong class="episode_title">Tut's Case Is Shut</strong> <span class="episode_date">(Original Airdate: September 29, 1966)</span><br />
                                King Tut deploys his injurious bug juice, slipping scarab mickeys to law enforcement! Our Peerless Pair must nail this nefarious no-good before the powerful potion gets in the water supply. Got buttermilk?
                            </li>
                        </ol>
                    </div>
                </div>
                <div class="season_b">
                    <div class="season_hdr">
                        <h3>
                            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/episodes/eg_s2_disc7.jpg" alt="Season 2, Disc 7: Episodes 43-50" class="block" /></h3>
                    </div>
                    <div class="season_txt">
                        <ol class="episode_details" style="counter-reset: item 42;">
                            <li><strong class="episode_title">The Greatest Mother Of Them All</strong> <span class="episode_date">(Original Airdate: October 5, 1966)</span><br />
                                When Ma Parker (Shelley Winters) and her corrupt kids go on a Gotham crime spree, Batman and Robin soundly round them up. It was all too easy, though, as Ma's gang takes over the prison and the Batmobile is rigged to become spare parts!
                            </li>
                            <li><strong class="episode_title">Ma Parker</strong> <span class="episode_date">(Original Airdate: October 6, 1966)</span><br />
                                Ma Parker and her criminal cohorts now have the perfect cover and base of operations. It will take all of Batman and Robin's deductive powers to crack this case, but Ma Parker is ready to sentence them to the electric chair.
                            </li>
                            <li><strong class="episode_title">The Clock King's Crazy Crimes</strong> <span class="episode_date">(Original Airdate: October 12, 1966)</span><br />
                                Clock King (Walter Slezak) and his Second Hands horde use an antique clock con to loot a jewelry store. Before you can say, "Holy timepiece!", the Dynamic Duo spring forward and fall prey to a giant hourglass and the sands of time!
                            </li>
                            <li><strong class="episode_title">The Clock King Gets Crowned</strong> <span class="episode_date">(Original Airdate: October 13, 1966)</span><br />
                                Clock King really wants to pilfer Bruce Wayne's priceless antique pocket watch collection, using an unwitting Aunt Harriet in the scheme. The clamorous clock caper unwinds into a race against time for Batman and Robin.
                            </li>
                            <li><strong class="episode_title">An Egg Grows In Gotham</strong> <span class="episode_date">(Original Airdate: October 19, 1966)</span><br />
                                An egg-ceptionally greedy Egghead (Vincent Price) and his lackeys are poised to poach Gotham's charter and use a loophole to own it all. The Dynamic Duo is onto him, but in a scathing scramble Egghead's Truth Machine could leave our heroes at wit's end.
                            </li>
                            <li><strong class="episode_title">The Yegg Foes In Gotham</strong> <span class="episode_date">(Original Airdate: October 20, 1966)</span><br />
                                Bruce and Dick's quick thinking closes Egghead's loophole, but he's not going to fry without a fight. He plans to plunder the City Treasury and run, but Batman and Robin are ready with a little bomb shelling.
                            </li>
                            <li><strong class="episode_title">The Devil's Fingers</strong> <span class="episode_date">(Original Airdate: October 26, 1966)</span><br />
                                While piano virtuoso, Chandell (Liberace), tickles the ivories for Aunt Harriet, a high-pitched heist hits Wayne Manor with Bruce and Dick away. The next concert strikes a false chord bringing the Dynamic Duo together, only to face the cutting room floor!
                            </li>
                            <li><strong class="episode_title">The Dead Ringers</strong> <span class="episode_date">(Original Airdate: October 27, 1966)</span><br />
                                Tablet_COMPLETE with imposters, dual identities and blackmail, this atrocious arrangement is full of sour notes and the Wayne fortune is in play. Batman and Robin have to get in tune quickly to make the maestros of mayhem face the music. 
                            </li>
                        </ol>
                    </div>
                </div>

            </div>

            <div class="clearfix bg_powie">
                <div class="season_a">
                    <div class="season_hdr">
                        <h3>
                            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/episodes/eg_s2_disc8.jpg" alt="Season 2, Disc 8: Episodes 51-58" class="block" /></h3>
                    </div>
                    <div class="season_txt">
                        <ol class="episode_details" style="counter-reset: item 50;">
                            <li><strong class="episode_title">Hizzonner The Penguin</strong> <span class="episode_date">(Original Airdate: November 2, 1966)</span><br />
                                Suddenly Penguin makes himself popular in the polls by endearing the citizens with good deeds in a beaky bid for Mayor.  Batman must throw his cowl into the ring, but the brash bird's campaign tactics may melt down the race.
                            </li>
                            <li><strong class="episode_title">Dizzoner The Penguin</strong> <span class="episode_date">(Original Airdate: November 3, 1966)</span><br />
                                Batman and Penguin continue their campaign debates and the feathers fly, but suddenly punks are pinching jewels at a convention. Penguin pops more crooks than Batman, and just might become Mayor! 
                            </li>
                            <li><strong class="episode_title">Green Ice</strong> <span class="episode_date">(Original Airdate: November 9, 1966)</span><br />
                                The malevolent Mr. Freeze (Otto Preminger) sends shivers throughout Gotham City with glacier of misdeeds all chipping away at Batman and Robin's reputation. Hot on his trial in the Batmobile, our Caped Crusaders are jumped and left to become frozen popsicles.
                            </li>
                            <li><strong class="episode_title">Deep Freeze</strong> <span class="episode_date">(Original Airdate: November 10, 1966)</span><br />
                                The cold crime wave continues with a vengeance as Freeze chills the air with threats to put all of Gotham on ice. Batman and Robin spring back into action to defrost felonious Freeze and his forces.
                            </li>
                            <li><strong class="episode_title">The Impractical Joker</strong> <span class="episode_date">(Original Airdate: November 16, 1966)</span><br />
                                The Joker just seems to want the keys to Gotham City handed to him as his latest crime spree sparks obscure clues. Meanwhile, back at the Batcave, the Dynamic Duo deciphers Joker's hideout only to be trapped lock, stock and spray wax!
                            </li>
                            <li><strong class="episode_title">The Joker's Provokers</strong> <span class="episode_date">(Original Airdate: November 17, 1966)</span><br />
                                Batman and Robin miraculously decode the true aim of our Clown Prince of Crime, but Joker has a time machine in a box that could play havoc. Using Alfred's help, our heroes go all out to put the fisticuffs on these felons.
                            </li>
                            <li><strong class="episode_title">Marsha, Queen Of Diamonds</strong> <span class="episode_date">(Original Airdate: November 23, 1966)</span><br />
                                Maleficent Marsha (Carolyn Jones) is as persuasive as she is power hungry. She's after the Batdiamond, which drives the Batcomputer at all costs. Once she has Robin under her spell, Batman must marry her to save him. Holy matrimony! A Dynamic Trio?
                            </li>
                            <li><strong class="episode_title">Marsha's Scheme Of Diamonds</strong> <span class="episode_date">(Original Airdate: November 24, 1966)</span><br />
                                Aunt Harriet and Alfred must save Batman from the altar, but the Queen Of Diamond will stop at nothing and seeks more powerful potions to pour. And horrors, it appears she's cut our Crusaders down to size — two caped toads!
                            </li>
                        </ol>
                    </div>
                </div>
                <div class="season_b">
                    <div class="season_hdr">
                        <h3>
                            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/episodes/eg_s2_disc9.jpg" alt="Season 2, Disc 9: Episodes 59-64" class="block" /></h3>
                    </div>
                    <div class="season_txt">
                        <ol class="episode_details" style="counter-reset: item 58;">
                            <li><strong class="episode_title">Come Back, Shame</strong> <span class="episode_date">(Original Airdate: November 30, 1966)</span><br />
                                Crusty cowpoke Shame (Cliff Robertson) is all over town stealing hot car parts so he can outrun the Batmobile with a fast truck. In a clever twist, Wayne's limo is used to snare the hombre, but Shame gets a drop on the Duo and they're staked out!
                            </li>
                            <li><strong class="episode_title">It's How You Play The Game</strong> <span class="episode_date">(Original Airdate: December 1, 1966)</span><br />
                                Shame's true aim is on four prize bulls worth over a million dollars, and he rustles them away easily in his super truck. Batman steers his attention to the K.O. Corral and feeding time, where he expects a final showdown.
                            </li>
                            <li><strong class="episode_title">The Penguin's Nest</strong> <span class="episode_date">(Original Airdate: December 7, 1966)</span><br />
                                Penguin opens a high-end eatery, but with a catch. Wealthy patrons write their own orders, providing the crafty con with a flock of samples. Now if he can get arrested, it's off to forge a perfect prison partnership!
                            </li>
                            <li><strong class="episode_title">The Bird's Last Jest</strong> <span class="episode_date">(Original Airdate: December 8, 1966)</span><br />
                                Penguin can't seem to get back to prison and partner with Ballpoint Baxter to forge checks, so the boisterous Bird-man and his ruffian restaurateurs serve up a desperate ransom — pie a la Alfred!
                            </li>
                            <li><strong class="episode_title">The Cat's Meow</strong> <span class="episode_date">(Original Airdate: December 14, 1966)</span><br />
                                Catwoman strays back into annoying action, and plans to steal the voices of rave English rockers. As she and her larcenous litter try to get near their target, Batman and Robin rush to foil the felines only to be subjected to echo chamber agony.
                            </li>
                            <li><strong class="episode_title">The Bat's Kow Tow</strong> <span class="episode_date">(Original Airdate: December 15, 1966)</span><br />
                                Sadly, Catwoman's Voice-Eraser works and she steals Chad and Jeremy's voices for a ravenous ransom. Batman and Robin trace where her calls are coming from, but Catwoman's lethal sonic gun stands in the way of justice.
                            </li>
                        </ol>
                    </div>
                </div>
            </div>



            <div class="clearfix bg_thwack">
                <div class="season_a">
                    <div class="season_hdr">
                        <h3>
                            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/episodes/eg_s2_disc10.jpg" alt="Season 2, Disc 10: Episodes 65-72" class="block" /></h3>
                    </div>
                    <div class="season_txt">
                        <ol class="episode_details" style="counter-reset: item 64;">
                            <li><strong class="episode_title">The Puzzles Are Coming</strong> <span class="episode_date">(Original Airdate: December 21, 1966)</span><br />
                                This Puzzler (Maurice Evans) is perfidious as his baffling clues balloon into a jewel heist at the christening of a jet. Batman and Robin retire to deconstruct another puzzling clue that leads them to Puzzler's hideout, where they're headed for a fall!
                            </li>
                            <li><strong class="episode_title">The Duo Is Slumming</strong> <span class="episode_date">(Original Airdate: December 22, 1966)</span><br />
                                The real prize for Puzzler is the super-jet belonging to Artemus Knab, which he intends to hold for ransom. The Dynamic Duo takes off to ground him, but Puzzler's nefarious crew promises them a hard landing.
                            </li>
                            <li><strong class="episode_title">The Sandman Cometh</strong> <span class="episode_date">(Original Airdate: December 28, 1966)</span><br />
                                Sandman (Michael Rennie) and Catwoman form a devious duo bent on absconding with the noodle fortune of J. Pauline Spaghetti, who hasn't slept in seven years. In pursuit, our Caped Crusaders are cuffed by henchmen and about to be needled.
                            </li>
                            <li><strong class="episode_title">The Catwoman Goeth</strong> <span class="episode_date">(Original Airdate: December 29, 1966)</span><br />
                                With Robin now trapped in the labyrinthine Catmaze, and Batman knowing a deceitful double-cross is brewing, it's time to put a stop payment on Sandman's ruse and deposit him in prison.
                            </li>
                            <li><strong class="episode_title">The Contaminated Cowl</strong> <span class="episode_date">(Original Airdate: January 4, 1967)</span><br />
                                The shyster of chapeaus is on the loose again, and ready to increase his mammoth collection. He aims to carry off with Batman's cowl, leaving the Dynamic Duo to be nuked by deadly X-rays.
                            </li>
                            <li><strong class="episode_title">The Mad Hatter Runs Afoul</strong> <span class="episode_date">(Original Airdate: January 5, 1967)</span><br />
                                In a clever swap, Mad Hatter thinks his plan is succeeding. Unaware Batman's cowl can be traced; the hapless Hatter and his thugs try hiding in the water tower ready to get soaked.
                            </li>
                            <li><strong class="episode_title">The Zodiac Crimes</strong> <span class="episode_date">(Original Airdate: January 11, 1967)</span><br />
                                Joker and Penguin team up for a wave of crimes based on the signs of the Zodiac. Trying to stay one step ahead of the thievery, Penguin gets caged, but the Dynamic Duo is about to be crushed by the museum's giant meteorite.
                            </li>
                            <li><strong class="episode_title">The Joker's Hard Times</strong> <span class="episode_date">(Original Airdate: January 12, 1967)</span><br />
                                Joker continues his rapacious rip-offs with the famous Golden Scorpion from the jewelry shop, but Batman and Robin are back in the hunt. As Joker nets a priceless fish (Pisces) from an exhibition, he nets our heroes to clam them up again!
                            </li>
                        </ol>
                    </div>
                </div>
                <div class="season_b">
                    <div class="season_hdr">
                        <h3>
                            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/episodes/eg_s2_disc11.jpg" alt="Season 2, Disc 11: Episodes 73-80" class="block" /></h3>
                    </div>
                    <div class="season_txt">
                        <ol class="episode_details" style="counter-reset: item 72;">
                            <li><strong class="episode_title">The Penguin Declines</strong> <span class="episode_date">(Original Airdate: January 18, 1967)</span><br />
                                Joker's men free Penguin back into the melee, while Batman and Robin masterfully undo the Joker-Jello affect on the water supply. The goons are now hidden in the Batmobile's trunk hoping to make Batman the goat (Capricorn)!
                            </li>
                            <li><strong class="episode_title">That Darn Catwoman</strong> <span class="episode_date">(Original Airdate: January 19, 1967)</span><br />
                                Catwoman's aide, Pussycat, pounces on Robin with Cataphrenic, making him their pawn in a plot to pilfer plans to the Gotham City Mint. Batman beats his way through only to be trussed up in a giant mousetrap Robin will spring.
                            </li>
                            <li><strong class="episode_title">Scat! Darn Catwoman</strong> <span class="episode_date">(Original Airdate: January 25, 1967)</span><br />
                                Catwoman offers Batman freedom if he takes her side. The Caped Crusader appears to comply by taking her drug, but as the crime wave continues anonymous tips force Catwoman to flee in the Batmobile! 
                            </li>
                            <li><strong class="episode_title">Penguin Is A Girl's Best Friend</strong> <span class="episode_date">(Original Airdate: January 26, 1967)</span><br />
                                Penguin's new front is a movie production company and he gets Batman and Robin signed to act in his film. After 100 takes with Marsha, Queen Of Diamonds in a kissing scene, our Peerless Pair catapult right into a corrupt caper! 
                            </li>
                            <li><strong class="episode_title">Penguin Sets A Trend</strong> <span class="episode_date">(Original Airdate: February 1, 1967)</span><br />
                                Batman and Robin rejoin the film company to stick close by Penguin, but the next scene calls for armor suits. Penguin clanks them onto huge electromagnets and the Dynamic Duo become scrap for the hydraulic crusher.
                            </li>
                            <li><strong class="episode_title">Penguin's Disastrous End</strong> <span class="episode_date">(Original Airdate: February 2, 1967)</span><br />
                                Penguin and Marsha move in for the final scene of his caper — at the treasury. As Batman, Robin and the authorities close in, the gang locks themselves in the vault with an escape plan worth its weight in gold bullion.
                            </li>
                            <li><strong class="episode_title">Batman's Anniversary</strong> <span class="episode_date">(Original Airdate: February 8, 1967)</span><br />
                                During an anniversary celebration of Batman's partnership with law enforcement, a disguised Riddler nabs a golden calf. New mystifying mayhem ensues, prompting a sinking feeling for the Dynamic Duo.
                            </li>
                            <li><strong class="episode_title">A Riddling Controversy</strong> <span class="episode_date">(Original Airdate: February 9, 1967)</span><br />
                                After two more heists, Riddler can finally secure the destructive De-Moleculizer which will force Gotham to meet his demands. Batman and Robin must solve a final riddle to neutralize the thorny threat.
                            </li>
                        </ol>
                    </div>
                </div>
            </div>
            <div class="clearfix bg_powie">
                <div class="season_a">
                    <div class="season_hdr">
                        <h3>
                            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/episodes/eg_s2_disc12.jpg" alt="Season 2, Disc 12: Episodes 81-88" class="block" /></h3>
                    </div>
                    <div class="season_txt">
                        <ol class="episode_details" style="counter-reset: item 80;">
                            <li><strong class="episode_title">The Joker's Last Laugh</strong> <span class="episode_date">(Original Airdate: February 15, 1967)</span><br />
                                Troubled by tainted taunts, it becomes clear Joker is back in business and circulating phony bills using a robot teller. Batman digs deeper and tries (as Bruce Wayne) to trick Joker, but in the ruse Robin becomes pressed for time!
                            </li>
                            <li><strong class="episode_title">The Joker's Epitaph</strong> <span class="episode_date">(Original Airdate: February 16, 1967)</span><br />
                                Bruce Wayne is complicit as Joker is named vice president of the bank on his signature, making it impossible for Batman to stop Joker's scheme without revealing his identity. Holy mental health, this is a conundrum!
                            </li>
                            <li><strong class="episode_title">Catwoman Goes To College</strong> <span class="episode_date">(Original Airdate: February 22, 1967)</span><br />
                                That crafty Catwoman is out on parole wanting to further her education, but she's still majoring in mayhem. In a cowled coup, Catwoman frames Batman, incites a student riot and percolates a liquid death for our Duo.
                            </li>
                            <li><strong class="episode_title">Batman Displays His Knowledge</strong> <span class="episode_date">(Original Airdate: February 23, 1967)</span><br />
                                Batman rubs Catwoman the wrong way by tricking her with cheap imitations of the opals she's snatched. The purr-fect revenge against Batman involves her irresistible poisonous perfume and a midnight rendezvous.
                            </li>
                            <li><strong class="episode_title">A Piece Of The Action</strong> <span class="episode_date">(Original Airdate: March 1, 1967)</span><br />
                                A rare stamp counterfeiting operation brings Batman and Robin face-to-face with Green Hornet and Kato (Van Williams and Bruce Lee), who are not above suspicion. The Dynamic Duo finally realize they're on the same side as their allies are about to be licked.
                            </li>
                            <li><strong class="episode_title">Batman's Satisfaction</strong> <span class="episode_date">(Original Airdate: March 2, 1967)</span><br />
                                Things come unglued as two sets of crime fighters go round and round, and the real counterfeiter, Colonel Gumm (Roger C. Carmel), is smoke-screening. Gumm grabs his bounty from the Stamps Exhibition and only the Divergent Duos can flatten him.
                            </li>
                            <li><strong class="episode_title">King Tut's Coup</strong> <span class="episode_date">(Original Airdate: March 8, 1967)</span><br />
                                King Tut is back after another bonk on his noggin, kidnapping Bruce Wayne's date to the Egyptian Ball. Next, he disposes of the Caped Crusader by submerging him in a bejeweled sarcophagus. 
                            </li>
                            <li><strong class="episode_title">Batman's Waterloo</strong> <span class="episode_date">(Original Airdate: March 9, 1967)</span><br />
                                Notorious nabob Tut has plans to boil Robin in oil and accept a huge ransom for the socialite he kidnapped. Batman will bring the cash alone, but can they all escape the royal boiling oil Tut has readied?
                            </li>
                        </ol>
                    </div>
                </div>
                <div class="season_b">
                    <div class="season_hdr">
                        <h3>
                            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/episodes/eg_s2_disc13.jpg" alt="Season 2, Disc 13: Episodes 89-94" class="block" /></h3>
                    </div>
                    <div class="season_txt">
                        <ol class="episode_details" style="counter-reset: item 88;">
                            <li><strong class="episode_title">Black Widow Strikes Again</strong> <span class="episode_date">(Original Airdate: March 15, 1967)</span><br />
                                After a series of alphabetical bank robberies, Black Widow (Tallulah Bankhead) is found to be electronically brainwashing her victims for loot. She then traps Batman and Robin in a giant, wicked web, leaving venomous black widow spiders to finish them!
                            </li>
                            <li><strong class="episode_title">Caught In The Spider's Den</strong> <span class="episode_date">(Original Airdate: March 16, 1967)</span><br />
                                The crafty Black Widow gives legs to more plotting, turns Batman into an accomplice and robs again — as the Dynamic Duo! Robin has little time to untangle his bonds and turn Black Widow's devious device back on her.
                            </li>
                            <li><strong class="episode_title">Pop Goes The Joker</strong> <span class="episode_date">(Original Airdate: March 22, 1967)</span><br />
                                The Joker knows the art of the steal, and his unexpected success as a pop artist leads him to open an art school with a sinister aim. Joker is after millionaires' art collections like Wayne's, and Robin's rescue attempt could cut him to pieces!
                            </li>
                            <li><strong class="episode_title">Flop Goes The Joker</strong> <span class="episode_date">(Original Airdate: March 23, 1967)</span><br />
                                The Joker works his wily ways toward swiping the Museum's Renaissance Collection, but Batman comes back swinging with a stroke of genius. Joker's not laughing when his misses the elevator and gets the shaft.
                            </li>
                            <li><strong class="episode_title">Ice Spy</strong> <span class="episode_date">(Original Airdate: March 29, 1967)</span><br />
                                An instant ice formula proves irresistible to frosty Mr. Freeze (Eli Wallach). He'll kidnap a professor to get it, demand a ransom and coolly keep both. The Dynamic Duo breaks into his hideout only to face vaporizing permanently into the Bruce Wayne Ice Arena!
                            </li>
                            <li><strong class="episode_title">The Duo Defy</strong> <span class="episode_date">(Original Airdate: March 30, 1967)</span><br />
                                Back at his iceberg headquarters, Mr. Freeze solidifies his threat to return the entire country to the ice age if his demands aren't met. Batman and Robin have a surprise for the hypothermic thief — a chilly reception in cold storage.
                            </li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
        <!-- END season2 -->





        <div id="season3">
            <div class="clearfix bg_thwack">
                <div class="season_a">
                    <div class="season_hdr">
                        <h3>
                            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/episodes/eg_s3_disc14.jpg" alt="Season 3, Disc 14: Episodes 95-102" class="block" /></h3>
                    </div>
                    <div class="season_txt">
                        <ol class="episode_details" style="counter-reset: item 94;">
                            <li><strong class="episode_title">Enter Batgirl, Exit Penguin</strong> <span class="episode_date">(Original Airdate: September 14, 1967)</span><br />
                                Penguin has kidnapped Commissioner Gordon's daughter in a flagrant ruse to marry her and gain immunity from charges. He's in for a flutter of surprises when a new secret crime fighter joins the Dynamic Duo to save the day.
                            </li>
                            <li><strong class="episode_title">Ring Around The Riddler</strong> <span class="episode_date">(Original Airdate: September 21, 1967)</span><br />
                                Tricky Riddler wants to take over the boxing game in Gotham with the mesmerizing help of Siren (Joan Collins). He challenges Batman and underhandedly gains the upper hand, but Batgirl is ready to punch out the perpetrators.
                            </li>
                            <li><strong class="episode_title">The Wail Of The Siren</strong> <span class="episode_date">(Original Airdate: September 28, 1967)</span><br />
                                Siren is persuasively power hungry. She trills the perfect tone on Commissioner Gordon and Batman in her songstress scheme to learn Batman's identity and control Wayne's fortune. Not so fast, chanteuse! Batgirl and Robin sing another tune.
                            </li>
                            <li><strong class="episode_title">The Sport Of Penguins</strong> <span class="episode_date">(Original Airdate: October 5, 1967)</span><br />
                                Perhaps shared affinities for parasols pair Penguin and Lola Lasagna in this crooked scheme to rig a horse race — or perhaps a large purse? Our crew of crusaders takes the offensive, literally glued to their seat.
                            </li>
                            <li><strong class="episode_title">A Horse Of Another Color</strong> <span class="episode_date">(Original Airdate: October 12, 1967)</span><br />
                                It's a long shot horse that will get scheming Penguin and Lola their scurrilous scratch. The fix is in, but Bruce enters his horse, Waynebeau, with a surprise jockey that leaves the preying Penguin with clipped wings!
                            </li>
                            <li><strong class="episode_title">The Unkindest Tut Of All</strong> <span class="episode_date">(Original Airdate: October 19, 1967)</span><br />
                                King Tut returns, predicting (and perpetrating) a series of sacks to win over the police. Baffled trying to prove Batman's identity, he'll settle for control of the world unless the Caped Crime Busters stop him.
                            </li>
                            <li><strong class="episode_title">Louie, The Lilac</strong> <span class="episode_date">(Original Airdate: October 26, 1967)</span><br />
                                Louie (Milton Berle) wants to control Gotham's flower children and corner the flower market right under Batman's nose. Amid stupefying sprays and vase-to-face frays, it's clearly time to send Louie back to the hot house.
                            </li>
                            <li><strong class="episode_title">The Ogg And I</strong> <span class="episode_date">(Original Airdate: November 2, 1967)</span><br />
                                Egghead joins up with Olga (Anne Baxter) to hatch a plot to kidnap Commissioner Gordon, demanding a 10¢ tax on every egg eaten. Olga also wants to marry the deviled Egghead and Batman, with our crime fighters then stymied by a veiled plot!
                            </li>
                        </ol>
                    </div>
                </div>
                <div class="season_b">
                    <div class="season_hdr">
                        <h3>
                            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/episodes/eg_s3_disc15.jpg" alt="Season 3, Disc 15: Episodes 103-110" class="block" /></h3>
                    </div>
                    <div class="season_txt">
                        <ol class="episode_details" style="counter-reset: item 102;">
                            <li><strong class="episode_title">How To Hatch A Dinosaur</strong> <span class="episode_date">(Original Airdate: November 9, 1967)</span><br />
                                Egghead, Olga and the Cossacks are coming! This time their odious idea is to radiate a Neosaurus egg and have the dinosaur dine on our dauntless defenders. Can this monstrous case be cracked?
                            </li>
                            <li><strong class="episode_title">Surf's Up! Joker's Under!</strong> <span class="episode_date">(Original Airdate: November 16, 1967)</span><br />
                                The Joker, hoping to win hearts and minds as a surfing champ, uses a kidnapping and devious devices to steal the skills. Contestants drop out and Batman and Robin drop in to make sure Joker wipes out.
                            </li>
                            <li><strong class="episode_title">The Londinium Larcenies</strong> <span class="episode_date">(Original Airdate: November 23, 1967)</span><br />
                                It's off to Londinium to solve the mystery of a Lord and Lady (Rudy Valee and Glynnis Johns) looting a priceless snuffbox collection. A closer look at Lord Ffog's estate leads to a noxious fog bomb going off in the makeshift Batcave under a rented manor.
                            </li>
                            <li><strong class="episode_title">The Foggiest Notion</strong> <span class="episode_date">(Original Airdate: November 30, 1967)</span><br />
                                When the air clears, suspicions shroud Lord Ffog and Lady Peasoup with Batman, Robin and Batgirl in pursuit. Clouds of man-made mist keep our heroes at bay, so they are free to steal away.
                            </li>
                            <li><strong class="episode_title">The Bloody Tower</strong> <span class="episode_date">(Original Airdate: December 7, 1967)</span><br />
                                Ffog and Peasoup are forging ahead to steal the Crown Jewels, throwing lethal fog pellets, death bees, and diversions at the Dynamic Trio. Can our heroes turn the tables and keep the Batmobile on the left side of the road?
                            </li>
                            <li><strong class="episode_title">Catwoman's Dressed To Kill</strong> <span class="episode_date">(Original Airdate: December 14, 1967)</span><br />
                                Catwoman (Eartha Kitt) is ready to filch the fashion world and snatch a solid gold dress. As the Terrific Trio bolt into action, Catwoman has the purr-fect pattern for pulling the wool over their eyes!
                            </li>
                            <li><strong class="episode_title">The Ogg Couple</strong> <span class="episode_date">(Original Airdate: December 21, 1967)</span><br />
                                Egghead and Olga are back to egg-stract valuables from the museum, and crib 500 pounds of caviar! Batgirl convinces Egghead to fink on Olga, but she's trussed in a trap only Batman and Robin can unscramble.
                            </li>
                            <li><strong class="episode_title">The Funny Feline Felonies</strong> <span class="episode_date">(Original Airdate: December 28, 1967)</span><br />
                                Joker and Catwoman link up over the lure of cash and more cash, and are ready to dynamite the Depository. With Batgirl's help, Batman and Robin jump the jester who offers them a numbing Joker-buzzer blast in return!
                            </li>
                        </ol>
                    </div>
                </div>

            </div>

            <div class="clearfix bg_powie">
                <div class="season_a">
                    <div class="season_hdr">
                        <h3>
                            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/episodes/eg_s3_disc16.jpg" alt="Season 3, Disc 16: Episodes 111-118" class="block" /></h3>
                    </div>
                    <div class="season_txt">
                        <ol class="episode_details" style="counter-reset: item 110;">
                            <li><strong class="episode_title">The Joke's On Catwoman</strong> <span class="episode_date">(Original Airdate: January 4, 1968)</span><br />
                                Arch-criminals Joker and Catwoman are going for the gunpowder they need for their heist, but our heroes blast apart their plans. Catwoman wants a fair trial, but jury tampering leads to courtroom chaos.
                            </li>
                            <li><strong class="episode_title">Louie's Lethal Lilac Time</strong> <span class="episode_date">(Original Airdate: January 11, 1968)</span><br />
                                Louie's lackeys kidnap Bruce and Dick so the lethal larcenist can corner the lilac perfume market. Batgirl comes to the rescue only to be dumped in a vat waiting for hot oil. Time to deploy the Instant Unfolding Batcostume Capsules!
                            </li>
                            <li><strong class="episode_title">Nora Clavicle And The Ladies' Crime Club</strong> <span class="episode_date">(Original Airdate: January 18, 1968)</span><br />
                                Commissioner Gordon is swept from office and replaced with Nora (Barbara Rush), the Mayor's wife. Soon she deploys a women-only force, and unleashes myriad explosive mechanical mice in an insurance scam. Holy pied piper! Who'll save Gotham?
                            </li>
                            <li><strong class="episode_title">Penguin's Clean Sweep</strong> <span class="episode_date">(Original Airdate: January 25, 1968)</span><br />
                                Penguin is back in circulation with bad bird bamboozle to contaminate currency with rare germs. As citizens toss their cash in the streets, Batman, Robin and Batgirl must make Penguin's wads worthless.
                            </li>
                            <li><strong class="episode_title">The Great Escape</strong> <span class="episode_date">(Original Airdate: February 1, 1968)</span><br />
                                Shame is sprung from prison with the help of Calamity Jan (Dina Merrill) and leaves a cryptic note for Batman to chew on. Shame's opera house holdup, backed by some handy Fear Gas, leaves the crafty cowpoke and gang free to roam and rob again.
                            </li>
                            <li><strong class="episode_title">The Great Train Robbery</strong> <span class="episode_date">(Original Airdate: February 8, 1968)</span><br />
                                Batgirl is exchanged for Frontier Fanny (Hermione Baddeley), as the stakes get higher to shut down Shame's real aim — The Great Train Robbery! Shame pulls it off, and all that's left is for Batman to challenge him to a mano-a-mano match up.
                            </li>
                            <li><strong class="episode_title">I'll Be A Mummy's Uncle</strong> <span class="episode_date">(Original Airdate: February 22, 1968)</span><br />
                                King Tut's need for Nilanium, the world's hardest metal, leads him to the vein beneath Wayne Manor. His mineshaft from the lot next door alarms Batman, but it's too late! The Tutlings have tunneled right into the Batcave!
                            </li>
                            <li><strong class="episode_title">The Joker's Flying Saucer</strong> <span class="episode_date">(Original Airdate: February 29, 1968)</span><br />
                                The crazy Clown Prince Of Crime has hateful high hopes of ruling the universe by building on a flying saucer scare. His need for the Wayne Foundation's lightweight beryllium results in Batgirl and Alfred's abduction…and Joker in orbit.
                            </li>
                        </ol>
                    </div>
                </div>
                <div class="season_b">
                    <div class="season_hdr">
                        <h3>
                            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/episodes/eg_s3_disc17.jpg" alt="Season 3, Disc 17: Episodes 119-120" class="block" /></h3>
                    </div>
                    <div class="season_txt">
                        <ol class="episode_details" style="counter-reset: item 118;">
                            <li><strong class="episode_title">The Entrancing Dr. Cassandra</strong> <span class="episode_date">(Original Airdate: March 7, 1968)</span><br />
                                Dr. Cassandra (Ida Lupino), husband Cabala (Howard Duff) and their confounded camouflage pills make it easy to rob banks, invisibly. To get the Terrific Trio out of the way, they bait a trap guaranteed to spring six arch-criminals for a Gotham crime spree!
                            </li>
                            <li><strong class="episode_title">Minerva, Mayhem And Millionaires</strong> <span class="episode_date">(Original Airdate: March 14, 1968)</span><br />
                                Glamorous and manipulative Minerva (Zsa Zsa Gabor) gets her hands on Bruce Wayne and his diamonds, using her Deepest Secret Extractor to learn his safe's combination. Unless Batman, Robin, Batgirl and Alfred wax Minerva, she'll skip with her swag!
                            </li>
                        </ol>
                        <%--<h2>Bonus Disc</h2>
                    <ol class="episode_details">
                        <li><strong class="episode_title">HOLY MEMORABILIA BATMAN!</strong>
                            <br />
                            Journey with Adam West as he guides us through the incredible museum-worthy collection of radio personality Ralph Garman. We’ll learn which items are the most sought after and what makes a collection stand out from the pack. Then we're off to Indiana to meet the Guinness World Record holder for the largest Batman collection known. Our one-of-a-kind tour tops off with a visit to the masterminds behind the full-scale Batmobile replica. The world of collecting memorabilia receives a great deal of attention from the fans of the Batman series. From the vintage to the new, from the hunt to the acquisition — collecting Batman ’66 memorabilia is a unique passion and arguably a way of life.
                        </li>
                        <li><strong class="episode_title">HANGING WITH BATMAN</strong>
                            <br />
                            A true slice of life with Adam West. Adam West has enjoyed an incredible character arc — just like Bruce Wayne and Batman. From retracing the steps of his youth dreaming of heroics to Adam’s days on the Batman TV series, learn how Batman embodied that vision. It’s not easy being Batman, yet Adam always kept the dream alive. He renewed that spirit in fans young and old alike, and this is a celebration of his achievement in his own words.

                        </li>
                        <li><strong class="episode_title">BATMANIA BORN! Building The World Of Batman </strong>
                            <br />
                            Explore the art and design behind the fiction. This must-see documentary brings to light the costumes, makeup, sets, music, writing, acting and directing that contributed to the greatest and possibly most influential television series ever. From new interviews with Adam West (Batman), Burt Ward (Robin) and Julie Newmar (Catwoman), learn how this series took the pop culture world by storm and defined a new genre of television. 
                        </li>
                        <li><strong class="episode_title">BATS OF THE ROUND TABLE</strong>
                            <br />
                            Imagine some of the top names in Hollywood gathering for an informal no-holds-barred conversation about the BATMAN television series. Featuring Kevin Smith, Phil Morris, Ralph Garman and Jim Lee, with guest of honor Adam West, the banter will be plenty. Who knows what will be revealed as we learn from famous fans just how important BATMAN the television series has been in their lives.
                        </li>
                        <li><strong class="episode_title">INVENTING BATMAN IN THE WORDS OF ADAM WEST</strong>
                            <br />
                            Adam West annotated scripts for the first and second episodes during production of the Batman TV series. Experience a rare fan treat as he discusses his original notes, the craft of acting and bringing Batman to life.
                        </li>
                        <li><strong class="episode_title">NA NA NA BATMAN!</strong>
                            <br />
                            We traveled all of Hollywood to ask the who’s who of the film and television industry to provide fond memories regarding Batman and the television series. A few even agreed to sing the theme song! Everyone loves the Batman TV series, and here's that tribute piece!

                        </li>
                    </ol>--%>
                    </div>
                </div>
            </div>
        </div>
        <!-- END season3 -->


        <!--#include file="bottomcta_complete.html"-->
    </div>
    <!-- END Tablet_COMPLETE COLLECTION -->
    


</div><!-- END container -->


<!--#include file="footer.html"-->

    <uc1:TrackingPixels runat="server" ID="TrackingPixels" />
</form>
</body>
</html>
