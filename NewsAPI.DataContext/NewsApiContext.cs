using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NewsAPI.Models;
using Newtonsoft.Json;

namespace NewsAPI.DataContext
{

    public class NewsApiContext : DbContext
    {
        public NewsApiContext(DbContextOptions<NewsApiContext> options) : base(options)
        {
        }

        public DbSet<Banner> Banners { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<FeaturedSection> FeaturedSections { get; set; }

        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FeaturedSectionArticles>()
                        .HasKey(pc => new { pc.ArticleID, pc.FeaturedSectionID });

            modelBuilder.Entity<FeaturedSectionArticles>()
                        .HasOne(a => a.Article)
                        .WithMany(c => c.FeaturedSectionArticles)
                        .HasForeignKey(a => a.ArticleID);

            modelBuilder.Entity<FeaturedSectionArticles>()
                        .HasOne(a => a.FeaturedSection)
                        .WithMany(c => c.FeaturedSectionArticles)
                        .HasForeignKey(a => a.FeaturedSectionID);

            modelBuilder.Entity<RelatedArticles>()
                        .HasKey(pc => new { pc.ArticleID, pc.RelatedArticleID });

            modelBuilder.Entity<RelatedArticles>()
                        .HasOne(a => a.Article)
                        .WithMany(c => c.RelatedArticles)
                        .HasForeignKey(a => a.ArticleID);

            modelBuilder.Entity<Category>()
                        .HasMany(c => c.Articles)
                        .WithOne(a => a.Category)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Article>()
                        .HasMany(a => a.Comments)
                        .WithOne(c => c.Article)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Banner>().HasData(
                new Banner { Id = 1, Message = "Josh Stickles T-Shirts and Temporary Tatoos Now Available in the CapTech Store." },
                new Banner { Id = 2, Message = "Available For a Limited Time Only, $19.99 + s/h." }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, ShortName = "world", DisplayName = "WORLD" },
                new Category { Id = 2, ShortName = "business", DisplayName = "BUSINESS" },
                new Category { Id = 3, ShortName = "sports", DisplayName = "SPORTS" },
                new Category { Id = 4, ShortName = "fashion", DisplayName = "FASHION & STYLE" },
                new Category { Id = 5, ShortName = "video", DisplayName = "VIDEO" },
                new Category { Id = 6, ShortName = "opinion", DisplayName = "OPINION" },
                new Category { Id = 7, ShortName = "travel", DisplayName = "TRAVEL" }
            );

            modelBuilder.Entity<Article>().HasData(GetArticlesFromJson().ToArray());

            modelBuilder.Entity<FeaturedSection>().HasData(
                new FeaturedSection { Id = 1, Description = "Aside" },
                new FeaturedSection { Id = 2, Description = "Main" },
                new FeaturedSection { Id = 3, Description = "Opinion" },
                new FeaturedSection { Id = 4, Description = "Travel" }
            );

            modelBuilder.Entity<RelatedArticles>().HasData(
                UpdateRelatedArticles(4, 10),
                UpdateRelatedArticles(10, 4),
                UpdateRelatedArticles(6, 7),
                UpdateRelatedArticles(7, 6),
                UpdateRelatedArticles(8, 9),
                UpdateRelatedArticles(9, 8)
            );

            modelBuilder.Entity<FeaturedSectionArticles>().HasData(
                SetFeaturedArticle(1, 1),
                SetFeaturedArticle(1, 2),
                SetFeaturedArticle(2, 3),
                SetFeaturedArticle(2, 4),
                SetFeaturedArticle(2, 5),
                SetFeaturedArticle(3, 6),
                SetFeaturedArticle(3, 7),
                SetFeaturedArticle(4, 8),
                SetFeaturedArticle(4, 9)
            );
        }

        private FeaturedSectionArticles SetFeaturedArticle(int featureId, int articleId)
        {
            return new FeaturedSectionArticles { FeaturedSectionID = featureId, ArticleID = articleId };
        }

        private RelatedArticles UpdateRelatedArticles(int articleId, int relatedArticleId)
        {
            return new RelatedArticles { ArticleID = articleId, RelatedArticleID = relatedArticleId };
        }

        private IEnumerable<Article> GetArticlesFromJson()
        {
            var jsonString = @"[
  {
    ""id"": 1,
    ""headLine"": ""Josh Stickles Wins Olympics, Discovers Cure for Cancer, Fixes World Economy, All Before Breakfast."",
    ""snippet"": ""In what is likely to become the most productive morning in recorded history, Joshua Stickles won all of the events in the 2014 winter Olympics single-handedly before sketching out a cure for cancer on a napkin and texting it to Washington DC for validation and then, explained “rounding up”, thus fixing the world’s ailing economy."",
    ""fullStory"": ""Relaxing poolside and sipping a Cuba Libre, Josh Stickles reflects on a busy morning and ponders on what to do after lunch. In what is likely to become the most productive morning in recorded history, Josh Stickles won all of the events in the 2014 winter Olympics single-handedly before sketching out a cure for cancer on a napkin and texting it to Washington DC for validation and then, explained “rounding up”, thus fixing the world’s ailing economy. <br><br>In what is likely to become the most productive morning  recorded history, Joshua Stickles won all of the events in the 2014 winter Olympics single-handedly before sketching out a  cure for cancer on a napkin and texting it to Washington DC for validation and then, explained “rounding up”, thus fixing the world’s ailing economy. In what likely to become productive morning in recorded history, Joshua Stickles won all of the events in the 2014 winter Olympics single-handedly before sketching out a cure for cancer on a napkin and texting it to Washington DC for validation and then, explained “rounding up”, thus fixing the world’s ailing economy.  In what is likely to become the most productive morning in recorded history, Joshua Stickles won all of the events in the 2014 winter Olympics single-handedly before sketching out a cure for cancer on a napkin and texting it to Washington DC for validation and then, explained “rounding up”, thus fixing world’s ailing economy."",
    ""location"": ""Sochi"",
    ""nsfw"": false,
    ""categoryId"": 1,
    ""relatedArticleIds"": [
    ],
    ""hasVideoPlaceholder"": false,
    ""numberOfImages"": 1
  },
  {
    ""id"": 2,
    ""headLine"": ""Eric Schmidt Asks Stickles to Fix the “Google Problem”."",
    ""snippet"": ""Despite controlling everything, everywhere, Google has struggled with a thorny  technical problem. In a pre-dawn meeting at his Silicon Valley home, Google CEO, Eric Schmidt told a humble and attentive Joshua Stickles that, “the entire fate of the planet awaits your solution, Dr. Stickles”."",
    ""fullStory"": ""Direct trade selfies artisan locavore. Portland hoodie McSweeney's deep v slow-carb literally. Godard next level blog, street art American Apparel Austin meh kale chips you probably haven't heard of them pug. Umami direct trade 8-bit farm-to-table. Pickled fashion axe normcore, Intelligentsia you probably haven't heard of them cred cornhole four loko Odd Future gastropub yr sartorial sustainable. Pour-over kogi Helvetica next level. Polaroid Schlitz Banksy plaid occupy mustache. Paleo cray sriracha letterpress, raw denim craft beer Schlitz tousled cliche. Ennui retro mumblecore, Intelligentsia pug semiotics meggings. Quinoa XOXO Neutra lo-fi. Blog chia Etsy, Banksy try-hard Helvetica ennui. Church-key distillery meggings street art, roof party single-origin coffee freegan slow-carb. Blue Bottle tofu Odd Future, put a bird on it art party banh mi ethical vegan Austin banjo Pitchfork. Letterpress art party hashtag plaid, banjo yr bespoke lo-fi cornhole Terry Richardson. Asymmetrical Vice freegan banh mi. Chia actually Williamsburg four loko, plaid semiotics drinking vinegar occupy. Kitsch stumptown banjo Helvetica. Scenester High Life shabby chic Portland. Dreamcatcher pickled food truck, locavore umami banjo ethnic seitan Truffaut iPhone vinyl squid swag. Authentic irony keytar Shoreditch sartorial. Art party VHS Terry Richardson fashion axe, ethnic cornhole stumptown. Bicycle rights sustainable biodiesel, polaroid next level cornhole meggings occupy photo booth Odd Future. Aesthetic pop-up 8-bit mumblecore, Thundercats irony butcher shabby chic literally you probably haven't heard of them. Flannel roof party 90's direct trade tousled, flexitarian yr craft beer meggings distillery hoodie. Banh mi narwhal direct trade, photo booth art party lomo meggings street art asymmetrical vegan. Vegan tousled craft beer fanny pack, occupy paleo American Apparel Thundercats salvia flexitarian roof party stumptown quinoa Marfa ethnic. PBR ugh pour-over, tattooed cardigan bicycle rights banjo beard paleo flannel. Synth squid photo booth typewriter lo-fi, irony authentic drinking vinegar Etsy pop-up mumblecore."",
    ""location"": ""Palo Alto"",
    ""nsfw"": false,
    ""categoryId"": 2,
    ""relatedArticleIds"": [
    ],
    ""hasVideoPlaceholder"": false,
    ""numberOfImages"": 0
  },
  {
    ""id"": 3,
    ""headLine"": ""Stickles: “No One Should Be Using IE. Ever.”"",
    ""snippet"": ""Shares of Microsoft Corporation plunged today to ninety-nine cents after revered digital pundit, Joshua Stickles said today that, “No one on God’s green Earth should be using Internet Explorer. Not now, Not ever.”. Mr. Stickles went on to say that IE has been the proverbial thorn-in-the-flesh since it had been released in 1995. Mr. Stickles vitriolic diatribe can be viewed in the video above."",
    ""fullStory"": ""+1 DIY seitan, freegan asymmetrical umami vinyl mumblecore retro Austin fingerstache bitters shabby chic. Try-hard Cosby sweater Echo Park Pitchfork Odd Future readymade. Vegan tattooed cray fixie, ugh PBR brunch. Wes Anderson viral kogi letterpress put a bird on it yr hoodie. Williamsburg mustache vegan organic. Kogi beard viral, Williamsburg mlkshk retro actually kitsch drinking vinegar banh mi. Tonx semiotics Portland shabby chic, organic +1 bespoke slow-carb photo booth. Distillery Wes Anderson paleo,  VHS fashion axe meggings tote bag occupy four loko mustache ugh Carles freegan wolf. Mustache selvage YOLO, bitters ugh next level synth 3 wolf moon tattooed. Put a bird on it flexitarian keffiyeh before they sold out wolf. Helvetica McSweeney's photo booth bespoke, occupy organic keffiyeh authentic. Mustache meggings forage kitsch fingerstache. Skateboard post-ironic American Apparel cred Terry Richardson, pour-over you probably haven't heard of them VHS  pork belly farm-to-table scenester Schlitz four loko. Squid Williamsburg kale chips, deep v distillery craft beer tofu Bushwick. Typewriter +1 Pitchfork normcore cardigan Carles. Semiotics four loko blog, scenester umami plaid tote bag locavore forage brunch banjo ennui sartorial gentrify pour-over. Actually Echo Park YOLO, photo booth umami direct trade PBR&B paleo McSweeney's Shoreditch scenester street art disrupt roof party brunch. Wayfarers fashion axe organic before they sold out. Raw denim Brooklyn post-ironic, iPhone mixtape vegan XOXO. Bitters jean shorts pug, Williamsburg paleo tote bag irony Echo Park synth VHS cardigan kitsch. Paleo kitsch flexitarian chambray fanny pack, stumptown Shoreditch Pitchfork PBR&B hella Tonx whatever butcher photo booth. Pickled tofu dreamcatcher flexitarian. Bushwick fanny pack irony, leggings chia vinyl Godard actually locavore  PBR&B pickled. Asymmetrical Marfa Terry Richardson, kale chips single-origin coffee Brooklyn yr Helvetica. Actually Cosby sweater gastropub, swag banh mi asymmetrical polaroid leggings keytar synth. Normcore letterpress ennui bicycle rights literally Truffaut. Kitsch Shoreditch cardigan slow-carb, wayfarers post-ironic you probably haven't heard of them mixtape tofu gluten-free chia direct trade disrupt. Vice semiotics pork belly vinyl, PBR cornhole Pitchfork."",
    ""location"": ""Redmond, Washington"",
    ""nsfw"": true,
    ""categoryId"": 3,
    ""relatedArticleIds"": [
    ],
    ""hasVideoPlaceholder"": true,
    ""numberOfImages"": 0
  },
  {
    ""id"": 4,
    ""headLine"": ""NASDAQ Urges Stickles to Become the First Individual to I.P.O."",
    ""snippet"": ""While it has been the exclusive domain of corporations to file for initial public offerings, to date a single individual has never done so. At the urging of NASDAQ,, Kleiner Perkins, and Kim Kardassian, Josh Stickles just might be the first regular dude to file for an initial public offering. Citing the quiet period, Mr. Stickles had no comment."",
    ""fullStory"": ""XOXO before they sold out tofu raw denim Tumblr cornhole, Odd Future irony pork belly chambray. Wayfarers selfies farm-to-table, actually Neutra you probably haven't heard of them try-hard tote bag keytar Brooklyn narwhal hashtag direct trade bitters scenester. Chambray Tumblr 3 wolf moon pug, mumblecore Banksy slow-carb mlkshk aesthetic plaid bitters. Pickled twee plaid mixtape chia. Banksy twee tattooed Pitchfork meh Pinterest, viral banh mi jean shorts. Vice normcore ugh you probably haven't heard of them, asymmetrical jean shorts tofu Austin messenger bag lo-fi fixie. Roof party post-ironic typewriter disrupt Banksy, Tumblr Schlitz before they sold out leggings kale chips actually shabby chic pug. Craft beer butcher forage sriracha, tote bag Helvetica Schlitz food truck Godard fixie put a bird on it roof party Pinterest. Skateboard farm-to-table mixtape PBR Odd Future occupy Cosby sweater organic Shoreditch, artisan post-ironic. Irony readymade Echo Park synth. Keffiyeh chillwave farm-to-table meggings Odd Future. Biodiesel asymmetrical viral kogi pour-over, pork belly mlkshk trust fund fingerstache mumblecore. Mumblecore semiotics literally Echo Park vinyl. You probably haven't heard of them organic YOLO Wes Anderson. Shabby chic Cosby sweater banh mi ethical Odd Future stumptown wayfarers 8-bit. Kogi ethical plaid Neutra sriracha kitsch viral. Organic normcore cred, raw denim next level deep v before they sold out polaroid art party church-key. Keytar Helvetica single-origin coffee chillwave kitsch, try-hard PBR organic jean shorts Marfa skateboard. Wolf single-origin coffee master cleanse Thundercats sartorial pork belly, scenester umami sustainable Pinterest. Tote bag swag gentrify, ethical authentic skateboard kogi. 3 wolf moon chia cardigan, keffiyeh tofu keytar disrupt kitsch ennui ethnic Pitchfork. IPhone skateboard mlkshk kitsch, banjo hella cornhole tousled whatever ethnic semiotics Neutra trust fund keffiyeh leggings. American Apparel shabby chic chia, Echo Park squid High Life you probably haven't heard of them ennui VHS vegan viral ugh mlkshk. Swag sustainable Cosby sweater, chia jean shorts roof party wolf pork belly. Readymade authentic fixie polaroid try-hard, wolf locavore mlkshk kogi Marfa Wes Anderson single-origin coffee leggings freegan. Lomo polaroid direct trade chia. Post-ironic slow-carb authentic pug hella, Helvetica cred Terry Richardson cliche deep v food truck. Hoodie farm-to-table church-key, Tumblr +1 cardigan mlkshk chia quinoa Terry Richardson gluten-free."",
    ""location"": ""New York"",
    ""nsfw"": false,
    ""categoryId"": 4,
    ""relatedArticleIds"": [
      10
    ],
    ""hasVideoPlaceholder"": false,
    ""numberOfImages"": 0
  },
  {
    ""id"": 5,
    ""headLine"": ""Stickles Throws 200 Yard Pass, Catches It Himself And Runs to Score the Winning Touchdown. Also Wins Augusta Same Day."",
    ""snippet"": ""Although not a professional football player, Josh Stickles made sporting history today by throwing a 200 yard pass, running downfield and catching it himself then running it to score the winning touchdown in a game that can only be described as “historic” and “the stuff of barely believable legends”."",
    ""fullStory"": ""Slow-carb Helvetica Austin Odd Future food truck asymmetrical. Raw denim biodiesel banh mi, pop-up Bushwick keytar try-hard aesthetic leggings. Godard flannel church-key pork belly, vegan Austin selfies semiotics fingerstache VHS stumptown farm-to-table authentic street art. High Life cardigan twee, farm-to-table bicycle rights fixie McSweeney's bespoke small batch typewriter viral swag cornhole irony. Tonx Portland mlkshk, photo booth cornhole semiotics bitters you probably haven't heard of them. Etsy Shoreditch pickled food truck sustainable, raw denim keffiyeh yr Helvetica Tumblr occupy narwhal. Gluten-free organic four loko, distillery YOLO bitters Wes Anderson  kogi next level occupy tote bag tofu. Helvetica typewriter keytar, slow-carb tousled squid semiotics put a bird on it umami jean shorts try-hard polaroid vegan Pitchfork. Gluten-free street art Terry Richardson 8-bit artisan. Photo booth squid literally, American Apparel distillery skateboard +1 Marfa. Cray locavore quinoa bitters. Bitters trust fund before they sold out farm-to-table Blue Bottle. Austin bicycle rights tote bag dreamcatcher, occupy Terry Richardson 90's sustainable. Selvage lomo Tonx, banjo retro dreamcatcher PBR drinking vinegar pork belly banh mi craft beer aesthetic bicycle rights meh. Ugh cliche Portland swag sriracha, readymade food truck quinoa dreamcatcher lo-fi whatever brunch. Forage cardigan narwhal, banh mi swag slow-carb pop-up literally. Brooklyn squid twee Carles. Letterpress kale chips put a bird on it four loko, banh mi irony twee cornhole High Life before they sold out. Meh hashtag +1 Neutra, quinoa bitters fingerstache Godard mixtape asymmetrical. XOXO +1 Neutra pour-over, you probably haven't heard of them trust fund mixtape Tonx narwhal Carles. Tonx bicycle rights banjo, pug hoodie Cosby sweater retro pour-over slow-carb Etsy meggings. Terry Richardson salvia Godard, mustache semiotics put a bird on it Blue Bottle pour-over. Raw denim twee chia photo booth quinoa. Ethical flannel Helvetica Neutra, pug blog Thundercats beard occupy post-ironic asymmetrical yr banjo four loko retro. Flexitarian VHS polaroid, locavore cray Neutra iPhone wayfarers. Hella Schlitz brunch, dreamcatcher Banksy selvage shabby chic pop-up. Wayfarers tofu iPhone church-key, gentrify meh put a bird on it readymade squid food truck literally you probably haven't heard of them. Put a bird on it PBR&B XOXO whatever."",
    ""location"": ""East Rutherford, NJ"",
    ""nsfw"": false,
    ""categoryId"": 5,
    ""relatedArticleIds"": [
    ],
    ""hasVideoPlaceholder"": false,
    ""numberOfImages"": 1
  },
  {
    ""id"": 6,
    ""headLine"": ""IF STICKLES CAN’T FIX IT, IT ISN’T WORTH FIXING."",
    ""snippet"": ""In the heart of the nation’s capitol, the United States government has been working on a secret plan to fix everything, everywhere. Codenamed FEE, for Fix Everything Everywhere, this plan would fix everything including the government, which many would argue, in itself, is broken."",
    ""fullStory"": ""In the heart of the nation’s capitol, the United States government has been working on a secret plan to fix everything, everywhere. Codenamed FEE, for Fix Everything Everywhere, this plan would fix everything including the government, which many would argue, in itself, is broken.<br><br> In a small lab in Langley, Virginia, Dr. Josh Stickles has been quietly working on FEE on his free nights and weekends. Using only an iPhone, a few paper clips, and a roll of duct tape, Dr. Stickles has been able to create , with a single swipe on a smartphone, the ability for anyone to fix everything everywhere. The app is expected to cost $.99 on the iTunes App Store in July."",
    ""location"": ""Washington, DC"",
    ""nsfw"": false,
    ""categoryId"": 6,
    ""relatedArticleIds"": [
      7
    ],
    ""hasVideoPlaceholder"": false,
    ""numberOfImages"": 0
  },
  {
    ""id"": 7,
    ""headLine"": ""MAC RUMORS: NEXT iPHONE TO BE CALLED THE JOSH S."",
    ""snippet"": ""While many Apple rumors can be difficult to rank for accuracy, it is all but a done deal that the next iPhone will be called the iPhone Josh S. <br><br>Tim Cook, said during the most recent CES conference that, “We have looked it at every way possible and decided there is simply no good reason to NOT call it the iPhone Josh S so that is what we are going to do."",
    ""fullStory"": ""While many Apple rumors can be difficult to rank for accuracy, it is all but a done deal that the next iPhone will be called the iPhone Josh S. <br><br>Tim Cook, said during the most recent CES conference that, “We have looked it at every way possible and decided there is simply no good reason to NOT call it the iPhone Josh S so that is what we are going to do. Shares of Apple closed up $675.22 on the news in active trading."",
    ""location"": ""Cupertino, CA"",
    ""nsfw"": false,
    ""categoryId"": 6,
    ""relatedArticleIds"": [
      6
    ],
    ""hasVideoPlaceholder"": false,
    ""numberOfImages"": 1
  },
  {
    ""id"": 8,
    ""headLine"": ""Tours of Sticklesland Are Sold Out Until 2023."",
    ""snippet"": ""Keffiyeh brunch single-origin coffee kogi street art selvage. Lomo authentic flexitarian bitters, swag forage salvia pop-up sartorial. Brooklyn banh mi occupy Cosby sweater semiotics direct trade cliche, paleo leggings roof party four loko. Forage Shoreditch pop-up, direct trade Truffaut keffiyeh ethnic post-ironic dreamcatcher DIY narwhal readymade fashion axe bitters jean shorts. Hashtag bitters Bushwick, fixie trust fund photo booth you probably haven't heard of them keffiyeh Schlitz keytar chillwave semiotics. Ethnic Schlitz Williamsburg PBR&B, PBR readymade ethical . Disrupt typewriter Cosby sweater meggings normcore mixtape."",
    ""fullStory"": ""Keffiyeh brunch single-origin coffee kogi street art selvage. Lomo authentic flexitarian bitters, swag forage salvia pop-up sartorial. Brooklyn banh mi occupy Cosby sweater semiotics direct trade cliche, paleo leggings roof party four loko. Forage Shoreditch pop-up, direct trade Truffaut keffiyeh ethnic post-ironic dreamcatcher DIY narwhal readymade fashion axe bitters jean shorts. Hashtag bitters Bushwick, fixie trust fund photo booth you probably haven't heard of them keffiyeh Schlitz keytar chillwave semiotics. Ethnic Schlitz Williamsburg PBR&B, PBR readymade ethical . Disrupt typewriter Cosby sweater meggings normcore mixtape. Cred aesthetic lomo before they sold out readymade Vice meh iPhone. Tousled Williamsburg literally messenger bag roof party. Messenger bag 90's Etsy, Wes Anderson single-origin coffee cray Pinterest ethnic sriracha Cosby sweater actually four loko mustache. Echo Park organic pug fingerstache Shoreditch pour-over 3 wolf moon. Gluten-free viral pork belly, banh mi Vice normcore Banksy street art. Roof party leggings PBR&B, 3 wolf moon mumblecore hoodie ethnic master cleanse  disrupt skateboard street art art party. Thundercats Cosby sweater lomo hashtag shabby chic brunch plaid authentic. Bitters biodiesel food truck lomo vegan, Etsy fixie art party artisan occupy Cosby sweater. YOLO dreamcatcher asymmetrical XOXO. Stumptown butcher blog, cornhole seitan deep v next level occupy 8-bit PBR authentic fashion axe letterpress. Odd Future bespoke dreamcatcher iPhone wayfarers selfies keffiyeh Neutra Blue Bottle letterpress, scenester kogi aesthetic literally single-origin coffee. Terry Richardson selvage single-origin coffee direct trade McSweeney's Intelligentsia. Next level PBR aesthetic Blue Bottle, drinking vinegar deep v wolf PBR&B beard mixtape master cleanse 8-bit photo booth hoodie Brooklyn. Retro you probably haven't heard of them readymade Pinterest literally High Life whatever, slow-carb brunch sriracha lomo iPhone skateboard stumptown vinyl. Normcore gluten-free keffiyeh, actually hella High Life flannel tattooed. Keytar gluten-free tote bag, Schlitz deep v fixie High Life actually before they sold out skateboard ethical organic readymade. Intelligentsia XOXO PBR semiotics, chia Helvetica banh mi 90's Echo Park. Post-ironic biodiesel fixie Cosby sweater Odd Future. Distillery food truck mumblecore, fixie kitsch roof party next level fingerstache four loko flexitarian hella. Vinyl letterpress post-ironic, before they sold out gluten-free swag distillery jean shorts slow-carb narwhal Austin. Direct trade quinoa keytar, Shoreditch Truffaut PBR salvia lomo dreamcatcher scenester umami viral leggings."",
    ""location"": ""Orlando, FL"",
    ""nsfw"": false,
    ""categoryId"": 7,
    ""relatedArticleIds"": [
      9
    ],
    ""hasVideoPlaceholder"": false,
    ""numberOfImages"": 2
  },
  {
    ""id"": 9,
    ""headLine"": ""Hotel Review: The Stickle Four Seasons in Dubai and Jefferson City, MO Open."",
    ""snippet"": ""Josh Stickles announced two new hotels in his rapidly expanding empire of ultra-luxury hotels at Motel 6 prices. A seven room suite with private swimming pool in Dubai is expected to go for $12.93 per night, futher disrupting the luxury travel market."",
    ""fullStory"": ""Dreamcatcher farm-to-table semiotics readymade, swag drinking vinegar meggings squid. Butcher tattooed pour-over bespoke. Pop-up salvia chia, kitsch single-origin coffee jean shorts McSweeney's dreamcatcher letterpress ethnic. Etsy you probably haven't heard of them twee keffiyeh, before they sold out Carles 90's gastropub selvage beard retro roof party. Brunch swag cornhole disrupt, narwhal actually Portland paleo skateboard salvia. Shoreditch cliche flannel, Godard Portland irony narwhal you probably haven't heard of them quinoa Vice iPhone keytar kitsch. Portland +1 Odd Future brunch photo booth. Selvage Bushwick photo booth, small batch hoodie meh tattooed Williamsburg fixie post-ironic. VHS hoodie Vice, Cosby sweater vegan leggings 8-bit Truffaut mixtape DIY you probably haven't heard of them bicycle rights pug beard master cleanse. Blue Bottle selvage irony scenester Portland. Cornhole ethical swag Schlitz banh mi synth. Neutra Echo Park chillwave, viral bespoke  forage scenester single-origin coffee church-key retro normcore pour-over. Mixtape bespoke Neutra master cleanse retro Cosby sweater. Readymade PBR&B tousled chia Carles. Lo-fi craft beer seitan, wolf synth iPhone ennui roof party paleo kale chips Thundercats ugh. Distillery tattooed meh sartorial try-hard cray. Kogi readymade cred banjo before they sold out food truck salvia. You probably haven't heard of them mustache fashion axe sustainable. Narwhal Austin four loko, ethnic McSweeney's pickled literally Bushwick. Shoreditch skateboard Truffaut beard, Etsy American Apparel squid tofu roof party lo-fi letterpress 3 wolf moon butcher asymmetrical pour-over. Freegan ennui VHS +1, post-ironic shabby chic semiotics. Odd Future lomo cornhole small batch mlkshk tote bag. Synth Terry Richardson keffiyeh mumblecore Pitchfork sriracha Pinterest. Raw denim quinoa hoodie, art party Vice kale chips Intelligentsia normcore . Ethical semiotics fingerstache, Tonx chillwave Marfa Echo Park. Banksy craft beer ennui four loko, cardigan cliche fashion axe. Pug Intelligentsia VHS, Carles aesthetic cliche pickled hella plaid kale chips Terry Richardson polaroid synth. Leggings kogi cardigan Wes Anderson, tousled fashion axe Odd Future Vice 90's butcher farm-to-table umami direct trade."",
    ""location"": ""Jefferson City, MO"",
    ""nsfw"": false,
    ""categoryId"": 7,
    ""relatedArticleIds"": [
      8
    ],
    ""hasVideoPlaceholder"": false,
    ""numberOfImages"": 1
  },
  {
    ""id"": 10,
    ""headLine"": ""Thousands Flock to the Home of a Man in Parma, Italy Who Claims to Have Grown a Gourd That Bears an Uncanny Resemblance to Josh Stickles."",
    ""snippet"": ""Josh Stickles announced two new hotels in his rapidly expanding empire of ultra-luxury hotels at Motel 6 prices. A seven room suite with private swimming pool in Dubai is expected to go for $12.93 per night, futher disrupting the luxury travel market."",
    ""fullStory"": ""Thousands of the nation’s curious and devoted showed up before dawn to perhaps catch a glimpse of what one man claims is, “the very image of Josh Stickles himself except it is a gourd”. The gourd, which is actually the size of a human head, by most accounts does indeed bear an uncanny resemblance to Dr. Joshua Stickles of the United States withthe notable exception that Mr. Stickles head is not green.<br> Thousands of the nation’s curious and devoted showed up before dawn to perhaps catch a glimpse of what one man claims is, “the very image of Josh Stickles himself except it is a gourd”. The gourd, which is actually the size of a human head, by most accounts does indeed bear an uncanny resemblance to Dr. Joshua Stickles of the United States with the notable exception that Mr. Stickles head is not green.<br> Thousands of the nation’s curious and devoted showed up before dawn to perhaps catch a glimpse of what one man claims is, “the very image of Josh Stickles himself except it is a gourd”. The gourd, which is actually the size of a human head, by most accounts does indeed bear an uncanny resemblance to Dr. Joshua Stickles of the United States with the notable exception that Mr. Stickles head is not green. Thousands of the nation’s curious and devoted showed up before dawn to perhaps catch a glimpse of what one man claims is, “the very image of Josh Stickles himself except it is a gourd”. The gourd, which is actually the size of a human head, by most accounts does indeed bear an uncanny resemblance to Dr. Joshua Stickles of the United States with the notable exception that Mr. Stickles head is not green."",
    ""location"": ""Parma Italy"",
    ""nsfw"": false,
    ""categoryId"": 4,
    ""relatedArticleIds"": [
      4
    ],
    ""hasVideoPlaceholder"": false,
    ""numberOfImages"": 1
  }
]";
            var articles = JsonConvert.DeserializeObject<List<Article>>(jsonString);
            return articles;
        }
    }
}
