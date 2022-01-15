using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTickets.Dal.Data;

public static class ExamTicketsSeedTestData
{
    public static async void SeedTestData(IServiceProvider provider, IConfiguration configuration, ILogger logger)
    {
        provider = provider.CreateScope().ServiceProvider;
        await using var context = new ExamTicketsContext(provider.GetRequiredService<DbContextOptions<ExamTicketsContext>>());
        if (context == null || context.Examens == null)
        {
            logger.LogError("Null ExamTicketsContext");
            throw new ArgumentNullException("Null ExamTicketsContext");
        }
        var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
        if (pendingMigrations.Any())
        {
            logger.LogInformation($"Applying migrations ExamTickets database: {string.Join(",", pendingMigrations)}");
            await context.Database.MigrateAsync();
        }
        if (context.Examens.Any())
        {
            logger.LogInformation("ExamTickets database contains data - database init with test data is not required");
            return;
        }
        logger.LogInformation("Begin writing test data to database ExamTicketsContext ...");

        var ga = new Group {Name = "А. Общие требования промышленной безопасности", Order = 1};
        context.Groups.AddRange(ga);
        await context.SaveChangesAsync();

        var ea1 = new Examen { Name = "А.1 (ПБ 115.15) Основы промышленной безопасности", Group = ga, Order = 1 };
        context.Examens.AddRange(ea1);
        await context.SaveChangesAsync();

        {
            var t = new Ticket { TicketText = "Билет № 1", Examen = ea1 };
            context.Tickets.Add(t);
            await context.SaveChangesAsync();
            var q1 = new Question { QuestionText = "В виде каких файлов должны формироваться электронные документы при подготовке отчета о производственном контроле?", Ticket = t};
            var q2 = new Question { QuestionText = "В какой срок со дня поступления требования страхователя об изменении условий договора обязательного страхования в связи с уменьшением страхового риска, включая уменьшение размера страховой премии, страховщик обязан рассмотреть такое требование?", Ticket = t};
            var q3 = new Question { QuestionText = "В каком случае эксплуатирующая организация вправе разрабатывать единый план мероприятий по локализации и ликвидации последствий аварий на опасных производственных объектах на несколько опасных объектов?", Ticket = t };
            var q4 = new Question { QuestionText = "Как называется один из видов деятельности в области промышленной безопасности, подлежащий лицензированию в соответствии с Федеральным законом от 04.05.2011 № 99-ФЗ «О лицензировании отдельных видов деятельности»?", Ticket = t };
            var q5 = new Question { QuestionText = "Какие сведения отражаются в заключении экспертизы промышленной безопасности по результатам экспертизы технического устройства?", Ticket = t };
            var q6 = new Question { QuestionText = "Какое определение соответствует понятию «авария», изложенному в Федеральном законе от 21.07.1997 № 116-ФЗ «О промышленной безопасности опасных производственных объектов»?", Ticket = t };
            var q7 = new Question { QuestionText = "Когда план мероприятий по локализации и ликвидации последствий аварий считается принятым?", Ticket = t };
            var q8 = new Question { QuestionText = "Кто утверждает декларацию промышленной безопасности?", Ticket = t };
            var q9 = new Question { QuestionText = "При каком условии событие признается страховым случаем?", Ticket = t };
            var q10 = new Question { QuestionText = "Что из указанного относится к обязанностям организации в области промышленной безопасности в соответствии с Федеральным законом от 21.07.1997 № 116-ФЗ «О промышленной безопасности опасных производственных объектов»?", Ticket = t };
            context.Questions.AddRange(q1, q2, q3, q4, q5, q6, q7, q8, q9, q10);
            await context.SaveChangesAsync();
            var q1o1 = new QuestionOption { OptionText = "В формате XML.", Question = q1, IsCorrect = true };
            var q1o2 = new QuestionOption { OptionText = "В формате DIF.", Question = q1 };
            var q1o3 = new QuestionOption { OptionText = "В форматах JPEG, TIFF, BMP, PDF.", Question = q1 };
            var q1o4 = new QuestionOption { OptionText = "В формате JSON.", Question = q1 };
            var q2o1 = new QuestionOption { OptionText = "Все ответы неверны.", Question = q2 };
            var q2o2 = new QuestionOption { OptionText = "5 рабочих дней.", Question = q2 };
            var q2o3 = new QuestionOption { OptionText = "10 рабочих дней.", Question = q2 };
            var q2o4 = new QuestionOption { OptionText = "30 рабочих дней.", Question = q2, IsCorrect = true };
            var q2o5 = new QuestionOption { OptionText = "20 рабочих дней.", Question = q2 };
            var q3o1 = new QuestionOption { OptionText = "В случае если объекты зарегистрированы в государственном реестре опасных производственных объектов.", Question = q3 };
            var q3o2 = new QuestionOption { OptionText = "План мероприятий разрабатывается на каждый опасный объект отдельно.", Question = q3 };
            var q3o3 = new QuestionOption { OptionText = "В случае если 2 и более объектов, эксплуатируемых одной организацией, расположены на одном земельном участке или на смежных земельных участках.", Question = q3, IsCorrect = true };
            var q3o4 = new QuestionOption { OptionText = "В случае если это регламентировано внутренней документацией организации.", Question = q3 };
            var q3o5 = new QuestionOption { OptionText = "Все ответы неверны.", Question = q3 };
            var q4o1 = new QuestionOption { OptionText = "Эксплуатация взрывопожароопасных производственных объектов.", Question = q4 };
            var q4o2 = new QuestionOption { OptionText = "Эксплуатация взрывопожароопасных и химически опасных производственных объектов I, II и III классов опасности.", Question = q4, IsCorrect = true };
            var q4o3 = new QuestionOption { OptionText = "Эксплуатация взрывопожароопасных и химически опасных производственных объектов всех классов опасности.", Question = q4 };
            var q4o4 = new QuestionOption { OptionText = "Эксплуатация химически опасных производственных объектов.", Question = q4 };
            var q5o1 = new QuestionOption { OptionText = "Выводы о правильности и достоверности выполненных расчетов по анализу риска, а также полноты учета факторов, влияющих на конечные результаты.", Question = q5 };
            var q5o2 = new QuestionOption { OptionText = "Обоснованность применяемых физико-математических моделей и использованных методов расчета последствий аварии и показателей риска.", Question = q5 };
            var q5o3 = new QuestionOption { OptionText = "Расчетные и аналитические процедуры оценки и прогнозирования технического состояния объекта экспертизы, включающие определение остаточного ресурса (срока службы).", Question = q5, IsCorrect = true };
            var q5o4 = new QuestionOption { OptionText = "Выводы о достаточности мер предотвращения проникновения на опасный производственный объект посторонних лиц.", Question = q5 };
            var q6o1 = new QuestionOption { OptionText = "Отказ или повреждение технических устройств, применяемых на опасном производственном объекте, отклонение от установленного режима технологического процесса.", Question = q6 };
            var q6o2 = new QuestionOption { OptionText = "Разрушение сооружений и (или) технических устройств, применяемых на опасном производственном объекте, неконтролируемые взрыв и (или) выброс опасных веществ.", Question = q6, IsCorrect = true };
            var q6o3 = new QuestionOption { OptionText = "Нарушение целостности или полное разрушение сооружений и технических устройств опасного производственного объекта при отсутствии взрыва либо выброса опасных веществ.", Question = q6 };
            var q6o4 = new QuestionOption { OptionText = "Контролируемое и (или) неконтролируемое горение, а также взрыв опасного производственного объекта.", Question = q6 };
            var q7o1 = new QuestionOption { OptionText = "После утверждения руководителями (заместителями руководителей) организаций, эксплуатирующих объекты, либо руководителями обособленных подразделений юридических лиц (в случаях, предусмотренных положениями о таких обособленных подразделениях) и согласования руководителями (заместителями руководителей, в должностные обязанности которых входит согласование планов мероприятий) профессиональных аварийно-спасательных служб или профессиональных аварийно-спасательных формирований, которые привлекаются для локализации и ликвидации последствий аварий на объекте.", Question = q7, IsCorrect = true };
            var q7o2 = new QuestionOption { OptionText = "После утверждения руководителем организации, эксплуатирующей опасные производственные объекты, или руководителями обособленных подразделений и согласования с органами Ростехнадзора.", Question = q7 };
            var q7o3 = new QuestionOption { OptionText = "После утверждения руководителем организации, эксплуатирующей опасные производственные объекты, или руководителями обособленных подразделений.", Question = q7 };
            var q8o1 = new QuestionOption { OptionText = "Руководитель эксплуатирующей организации совместно с территориальным органом Ростехнадзора.", Question = q8 };
            var q8o2 = new QuestionOption { OptionText = "Руководитель территориального органа федерального органа исполнительной власти в области промышленной безопасности или его заместители.", Question = q8 };
            var q8o3 = new QuestionOption { OptionText = "Руководитель организации, эксплуатирующей опасный производственный объект.", Question = q8, IsCorrect = true };
            var q8o4 = new QuestionOption { OptionText = "Руководитель экспертной организации, выполнившей экспертизу декларации промышленной безопасности.", Question = q8 };
            var q9o1 = new QuestionOption { OptionText = "Если в результате аварии на опасном объекте после окончания действия договора страхования причинен вред нескольким потерпевшим.", Question = q9 };
            var q9o2 = new QuestionOption { OptionText = "Если вред, причиненный в период действия договора страхования, является результатом последствий или продолжающегося воздействия аварии, произошедшей до заключения договора обязательного страхования.", Question = q9 };
            var q9o3 = new QuestionOption { OptionText = "Если причинен вред потерпевшим, явившийся результатом последствий воздействия аварии, произошедшей в период действия договора обязательного страхования, которое влечет за собой обязанность страховщика произвести страховую выплату потерпевшим.", Question = q9, IsCorrect = true };
            var q10o1 = new QuestionOption { OptionText = "Декларирование соответствия условий труда государственным нормативным требованиям охраны труда.", Question = q10 };
            var q10o2 = new QuestionOption { OptionText = "Обеспечение работников опасного производственного объекта средствами индивидуальной защиты.", Question = q10 };
            var q10o3 = new QuestionOption { OptionText = "Разработка локальных нормативных документов по охране труда.", Question = q10 };
            var q10o4 = new QuestionOption { OptionText = "Наличие на опасном производственном объекте нормативных правовых актов, устанавливающих требования промышленной безопасности, а также правил ведения работ на опасном производственном объекте.", Question = q10, IsCorrect = true };
            context.QuestionOptions.AddRange( q1o1, q1o2, q1o3, q1o4, q2o1, q2o2, q2o3, q2o4, q2o5, q3o1, q3o2, q3o3, q3o4, q3o5, q4o1, q4o2, q4o3, q4o4, q5o1, q5o2, q5o3, q5o4,
                q6o1, q6o2, q6o3, q6o4, q7o1, q7o2, q7o3, q8o1, q8o2, q8o3, q8o4, q9o1, q9o2, q9o3, q10o1, q10o2, q10o3, q10o4);
            await context.SaveChangesAsync();
        }
        {
            var t = new Ticket { TicketText = "Билет № 2", Examen = ea1 };
            context.Tickets.Add(t);
            await context.SaveChangesAsync();
            var q1 = new Question { QuestionText = "В какие сроки эксплуатирующая организация представляет в Ростехнадзор или его территориальные органы сведения об организации производственного контроля за соблюдением требований промышленной безопасности?", Ticket = t };
            var q2 = new Question { QuestionText = "В какой срок эксплуатирующие организации и индивидуальные предприниматели обязаны предоставить в регистрирующий орган сведения, характеризующие опасные производственные объекты?", Ticket = t };
            var q3 = new Question { QuestionText = "В каком случае юридическое лицо признается виновным в совершении административного правонарушения?", Ticket = t };
            var q4 = new Question { QuestionText = "Как производится ввод в эксплуатацию опасного производственного объекта?", Ticket = t };
            var q5 = new Question { QuestionText = "Какие сроки действия планов мероприятий по локализации и ликвидации последствий аварий установлены для объектов I класса опасности (за исключением объектов, на которых ведутся горные работы)?", Ticket = t };
            var q6 = new Question { QuestionText = "Какое право не предоставлено должностным лицам Ростехнадзора при осуществлении федерального государственного надзора в области промышленной безопасности?", Ticket = t };
            var q7 = new Question { QuestionText = "Когда положение о производственном контроле считается принятым?", Ticket = t };
            var q8 = new Question { QuestionText = "Кто утверждает планы мероприятий по локализации и ликвидации последствий аварий на опасных производственных объектах? Выберите два варианта ответа.\nА) Руководитель (заместители руководителей) организаций, эксплуатирующих объекты.\nБ) Руководители профессиональных аварийно - спасательных служб или профессиональных аварийно-спасательных формирований.\nВ) Руководители обособленных подразделений юридических лиц(в случаях, предусмотренных положениями о таких обособленных подразделениях).\nГ) Инспектор Ростехнадзора.\nД) Все ответы неверны.", Ticket = t };
            var q9 = new Question { QuestionText = "При строительстве и реконструкции каких объектов капитального строительства осуществляется государственный строительный надзор?", Ticket = t };
            var q10 = new Question { QuestionText = "Что не является предметом государственного строительного надзора?", Ticket = t };
            context.Questions.AddRange(q1, q2, q3, q4, q5, q6, q7, q8, q9, q10);
            await context.SaveChangesAsync();
            var q1o1 = new QuestionOption { OptionText = "Ежегодно, в течение I квартала текущего года", Question = q1 };
            var q1o2 = new QuestionOption { OptionText = "Ежегодно, до 1 апреля соответствующего календарного года", Question = q1, IsCorrect = true };
            var q1o3 = new QuestionOption { OptionText = "Раз в полгода, не позднее 15 числа месяца, следующего за отчетным периодом", Question = q1 };
            var q1o4 = new QuestionOption { OptionText = "Ежегодно, не позднее 1 февраля текущего года", Question = q1 };
            var q2o1 = new QuestionOption { OptionText = "Не позднее 10 рабочих дней со дня начала их эксплуатации.", Question = q2, IsCorrect = true };
            var q2o2 = new QuestionOption { OptionText = "Срок предоставления сведений не регламентирован.", Question = q2 };
            var q2o3 = new QuestionOption { OptionText = "Не позднее 30 рабочих дней со дня начала их эксплуатации.", Question = q2 };
            var q2o4 = new QuestionOption { OptionText = "Не позднее трех месяцев с даты начала эксплуатации.", Question = q2 };
            var q3o1 = new QuestionOption { OptionText = "Если юридическое лицо признало факт совершения административного правонарушения.", Question = q3 };
            var q3o2 = new QuestionOption { OptionText = "Если будет установлено, что у него имелась возможность для соблюдения правил и норм, за нарушение которых предусмотрена административная ответственность, но им не были приняты все зависящие от него меры по их соблюдению.", Question = q3, IsCorrect = true };
            var q3o3 = new QuestionOption { OptionText = "Если должностное лицо, рассматривающее дело об административном правонарушении, уверено в виновности юридического лица.", Question = q3 };
            var q4o1 = new QuestionOption { OptionText = "В порядке, установленном законодательством Российской Федерации о техническом регулировании.", Question = q4 };
            var q4o2 = new QuestionOption { OptionText = "В порядке, установленном законодательством Российской Федерации о промышленной безопасности.", Question = q4 };
            var q4o3 = new QuestionOption { OptionText = "В порядке, установленном законодательством Российской Федерации о градостроительной деятельности.", Question = q4, IsCorrect = true };
            var q5o1 = new QuestionOption { OptionText = "5 лет.", Question = q5, IsCorrect = true };
            var q5o2 = new QuestionOption { OptionText = "3 года.", Question = q5 };
            var q5o3 = new QuestionOption { OptionText = "1 год.", Question = q5 };
            var q5o4 = new QuestionOption { OptionText = "2 года.", Question = q5 };
            var q6o1 = new QuestionOption { OptionText = "Направлять в уполномоченные органы материалы, связанные с нарушениями обязательных требований, для решения вопросов о возбуждении уголовных дел по признакам преступлений.", Question = q6 };
            var q6o2 = new QuestionOption { OptionText = "Составлять протоколы об административных правонарушениях, связанных с нарушениями обязательных требований, рассматривать дела об указанных административных правонарушениях и принимать меры по предотвращению таких нарушений.", Question = q6 };
            var q6o3 = new QuestionOption { OptionText = "Давать указания о выводе людей с рабочих мест в случае угрозы жизни и здоровью работников.", Question = q6 };
            var q6o4 = new QuestionOption { OptionText = "Посещать организации, эксплуатирующие опасные производственные объекты, при наличии служебного удостоверения и копии приказа о проведении проверки.", Question = q6 };
            var q6o5 = new QuestionOption { OptionText = "Выдавать лицензии на отдельные виды деятельности, связанные с повышенной опасностью промышленных производств.", Question = q6, IsCorrect = true };
            var q7o1 = new QuestionOption { OptionText = "После утверждения его руководителем эксплуатирующей организации (руководителем обособленного подразделения юридического лица), индивидуальным предпринимателем и согласования с центральным аппаратом Ростехнадзора.", Question = q7 };
            var q7o2 = new QuestionOption { OptionText = "После утверждения его руководителем эксплуатирующей организации (руководителем обособленного подразделения юридического лица), индивидуальным предпринимателем и согласования с территориальным органом Ростехнадзора.", Question = q7 };
            var q7o3 = new QuestionOption { OptionText = "После утверждения его территориальным органом Ростехнадзора.", Question = q7 };
            var q7o4 = new QuestionOption { OptionText = "После утверждения его руководителем эксплуатирующей организации (руководителем обособленного подразделения юридического лица), индивидуальным предпринимателем.", Question = q7, IsCorrect = true };
            var q8o1 = new QuestionOption { OptionText = "A и Б", Question = q8 };
            var q8o2 = new QuestionOption { OptionText = "А и В", Question = q8, IsCorrect = true };
            var q8o3 = new QuestionOption { OptionText = "А и Г", Question = q8 };
            var q8o4 = new QuestionOption { OptionText = "Б и В", Question = q8 };
            var q8o5 = new QuestionOption { OptionText = "Б и Г", Question = q8 };
            var q8o6 = new QuestionOption { OptionText = "В и Г", Question = q8 };
            var q8o7 = new QuestionOption { OptionText = "Д", Question = q8 };
            var q9o1 = new QuestionOption { OptionText = "При строительстве любых объектов.", Question = q9 };
            var q9o2 = new QuestionOption { OptionText = "Только при строительстве объектов, общая площадь которых составляет более 1500 м?.", Question = q9 };
            var q9o3 = new QuestionOption { OptionText = "Только при строительстве объектов, которые в соответствии с Градостроительным кодексом Российской Федерации являются особо опасными, технически сложными или уникальными.", Question = q9 };
            var q9o4 = new QuestionOption { OptionText = "При строительстве объектов капитального строительства, проектная документация которых подлежит экспертизе в соответствии со статьей 49 Градостроительного кодекса Российской Федерации.", Question = q9, IsCorrect = true };
            var q10o1 = new QuestionOption { OptionText = "Наличие разрешения на строительство.", Question = q10 };
            var q10o2 = new QuestionOption { OptionText = "Выполнение работ по договорам о строительстве, реконструкции, капитальном ремонте объектов капитального строительства, заключенным с застройщиком, техническим заказчиком, лицом, ответственным за эксплуатацию здания, сооружения, региональным оператором, только индивидуальными предпринимателями или юридическими лицами, которые являются членами саморегулируемых организаций в области строительства, реконструкции, капитального ремонта объектов капитального строительства.", Question = q10 };
            var q10o3 = new QuestionOption { OptionText = "Наличие декларации промышленной безопасности.", Question = q10, IsCorrect = true };
            var q10o4 = new QuestionOption { OptionText = "Соответствие строительных материалов, применяемых в процессе строительства, реконструкции объекта капитального строительства требованиям технических регламентов, проектной документации.", Question = q10 };
            context.QuestionOptions.AddRange(q1o1, q1o2, q1o3, q1o4, q2o1, q2o2, q2o3, q2o4, q3o1, q3o2, q3o3, q4o1, q4o2, q4o3, q5o1, q5o2, q5o3, q5o4,
                q6o1, q6o2, q6o3, q6o4, q6o5, q7o1, q7o2, q7o3, q7o4, q8o1, q8o2, q8o3, q8o4, q8o5, q8o6, q8o7, q9o1, q9o2, q9o3, q9o4, q10o1, q10o2, q10o3, q10o4);
            await context.SaveChangesAsync();
        }
        {
            var t = new Ticket { TicketText = "Билет № 3", Examen = ea1 };
            context.Tickets.Add(t);
            await context.SaveChangesAsync();
            var q1 = new Question { QuestionText = "В какие федеральные органы исполнительной власти заявитель, предполагающий выполнение работ (оказание услуг) при эксплуатации взрывопожароопасных и химически опасных производственных объектов IV класса опасности, должен представлять уведомления о начале осуществления своей деятельности?", Ticket = t };
            var q2 = new Question { QuestionText = "В какой форме осуществляется обязательная оценка соответствия зданий и сооружений, а также связанных со зданиями и с сооружениями процессов эксплуатации?", Ticket = t };
            var q3 = new Question { QuestionText = "В отношении каких из перечисленных объектов капитального строительства государственная экспертиза проектов не проводится?", Ticket = t };
            var q4 = new Question { QuestionText = "Какая административная ответственность предусмотрена законодательством Российской Федерации за нарушение должностными лицами требований промышленной безопасности или лицензионных требований на осуществление видов деятельности в области промышленной безопасности?", Ticket = t };
            var q5 = new Question { QuestionText = "Какие сроки действия планов мероприятий по локализации и ликвидации последствий аварий установлены для объектов II класса опасности (за исключением объектов, на которых ведутся горные работы)?", Ticket = t };
            var q6 = new Question { QuestionText = "Какой из перечисленных случаев не может являться основанием для исключения объекта из государственного реестра опасных производственных объектов?", Ticket = t };
            var q7 = new Question { QuestionText = "Кому вменена обязанность страховать свою ответственность за причинение вреда в результате аварии на опасном объекте в соответствии с Федеральным законом от 27.07.2010 № 225-ФЗ «Об обязательном страховании гражданской ответственности владельца опасного объекта за причинение вреда в результате аварии на опасном объекте»?", Ticket = t };
            var q8 = new Question { QuestionText = "Кто является владельцем опасного объекта в терминологии Федерального закона от 27.07.2010 №225-ФЗ «Об обязательном страховании гражданской ответственности владельцев опасных объектов за причинение вреда в результате аварии на опасном объекте»?", Ticket = t };
            var q9 = new Question { QuestionText = "Разработка каких документов в рамках организации документационного обеспечения систем управления промышленной безопасностью не предусмотрена в нормативном правовом акте?", Ticket = t };
            var q10 = new Question { QuestionText = "Что обязан сделать лицензиат, если он намерен изменить адрес места осуществления лицензируемого вида деятельности?", Ticket = t };
            context.Questions.AddRange(q1, q2, q3, q4, q5, q6, q7, q8, q9, q10);
            await context.SaveChangesAsync();
            var q1o1 = new QuestionOption { OptionText = "В Федеральную службу по надзору в сфере здравоохранения.", Question = q1 };
            var q1o2 = new QuestionOption { OptionText = "В Федеральное медико-биологическое агентство.", Question = q1 };
            var q1o3 = new QuestionOption { OptionText = "В Федеральное агентство по техническому регулированию и метрологии.", Question = q1 };
            var q1o4 = new QuestionOption { OptionText = "В Министерство Российской Федерации по делам гражданской обороны, чрезвычайным ситуациям и ликвидации последствий стихийных бедствий.", Question = q1 };
            var q1o5 = new QuestionOption { OptionText = "В Федеральную службу по труду и занятости.", Question = q1 };
            var q1o6 = new QuestionOption { OptionText = "В Федеральную службу по экологическому, технологическому и атомному надзору.", Question = q1, IsCorrect = true };
            var q2o1 = new QuestionOption { OptionText = "В форме эксплуатационного и государственного контроля (надзора).", Question = q2, IsCorrect = true };
            var q2o2 = new QuestionOption { OptionText = "В форме государственного строительного надзора и государственного контроля.", Question = q2 };
            var q2o3 = new QuestionOption { OptionText = "В форме производственного контроля.", Question = q2 };
            var q3o1 = new QuestionOption { OptionText = "Объектов, строительство, реконструкцию и (или) капитальный ремонт которых предполагается осуществлять в исключительной экономической зоне Российской Федерации, на континентальном шельфе Российской Федерации, во внутренних морских водах и в территориальном море Российской Федерации.", Question = q3 };
            var q3o2 = new QuestionOption { OptionText = "Объектов капитального строительства, в отношении которых не требуется получение разрешения на строительство.", Question = q3, IsCorrect = true };
            var q3o3 = new QuestionOption { OptionText = "Особо опасных, технически сложных и уникальных объектов.", Question = q3 };
            var q3o4 = new QuestionOption { OptionText = "Объектов, строительство, реконструкцию и (или) капитальный ремонт которых предполагается осуществлять на территориях двух и более субъектов Российской Федерации.", Question = q3 };
            var q4o1 = new QuestionOption { OptionText = "Административный арест на срок до 15 суток или административный штраф в размере до тридцати тысяч рублей.", Question = q4 };
            var q4o2 = new QuestionOption { OptionText = "Вынесение письменного предупреждения, о чем делается соответствующая отметка в личном деле привлеченного к ответственности лица, или штраф в размере до одного минимального размера оплаты труда.", Question = q4 };
            var q4o3 = new QuestionOption { OptionText = "Исправительные работы или административный штраф в размере до пятидесяти тысяч рублей.", Question = q4 };
            var q4o4 = new QuestionOption { OptionText = "Наложение административного штрафа в размере от двадцати до тридцати тысяч рублей или дисквалификация на срок от шести месяцев до одного года.", Question = q4, IsCorrect = true };
            var q5o1 = new QuestionOption { OptionText = "2 года.", Question = q5 };
            var q5o2 = new QuestionOption { OptionText = "3 года.", Question = q5 };
            var q5o3 = new QuestionOption { OptionText = "1 год.", Question = q5 };
            var q5o4 = new QuestionOption { OptionText = "5 лет.", Question = q5, IsCorrect = true };
            var q6o1 = new QuestionOption { OptionText = "Утрата объектом признаков опасности.", Question = q6 };
            var q6o2 = new QuestionOption { OptionText = "Грубое нарушение требований промышленной безопасности при эксплуатации опасного производственного объекта.", Question = q6, IsCorrect = true };
            var q6o3 = new QuestionOption { OptionText = "Изменение критериев отнесения объектов к категории опасных производственных объектов или требований к идентификации опасных производственных объектов.", Question = q6 };
            var q6o4 = new QuestionOption { OptionText = "Ликвидация объекта или вывод его из эксплуатации.", Question = q6 };
            var q7o1 = new QuestionOption { OptionText = "Проектным организациям.", Question = q7 };
            var q7o2 = new QuestionOption { OptionText = "Эксплуатирующим организациям независимо от того, являются они владельцами опасного объекта или нет.", Question = q7 };
            var q7o3 = new QuestionOption { OptionText = "Владельцам опасного объекта.", Question = q7, IsCorrect = true };
            var q7o4 = new QuestionOption { OptionText = "Экспертным организациям.", Question = q7 };
            var q8o1 = new QuestionOption { OptionText = "Юридическое лицо, владеющее опасным объектом на праве собственности.", Question = q8 };
            var q8o2 = new QuestionOption { OptionText = "Юридическое лицо или индивидуальный предприниматель, владеющие опасным объектом на праве собственности, праве хозяйственного ведения или праве оперативного управления либо на ином законном основании и осуществляющие эксплуатацию опасного объекта.", Question = q8, IsCorrect = true };
            var q8o3 = new QuestionOption { OptionText = "Юридические лица, владеющие опасным объектом на праве собственности, праве хозяйственного ведения или праве оперативного управления либо на ином законном основании, независимо от того, осуществляют они эксплуатацию опасного производственного объекта или нет.", Question = q8 };
            var q9o1 = new QuestionOption { OptionText = "Все ответы неверны.", Question = q9 };
            var q9o2 = new QuestionOption { OptionText = "План мероприятий по обеспечению промышленной безопасности на календарный год.", Question = q9 };
            var q9o3 = new QuestionOption { OptionText = "План работ по модернизации опасного производственного объекта.", Question = q9, IsCorrect = true };
            var q9o4 = new QuestionOption { OptionText = "План мероприятий по снижению риска аварий на опасном производственном объекте на срок более 1 календарного года.", Question = q9 };
            var q9o5 = new QuestionOption { OptionText = "Разработка всех перечисленных документов обязательна.", Question = q9 };
            var q10o1 = new QuestionOption { OptionText = "Подать заявление в лицензирующий орган о переоформлении лицензии.", Question = q10, IsCorrect = true };
            var q10o2 = new QuestionOption { OptionText = "Направить в лицензирующий орган уведомление о своих намерениях.", Question = q10 };
            var q10o3 = new QuestionOption { OptionText = "Подать заявление в лицензирующий орган о выдаче новой лицензии.", Question = q10 };
            context.QuestionOptions.AddRange(q1o1, q1o2, q1o3, q1o4, q1o5, q1o6, q2o1, q2o2, q2o3, q3o1, q3o2, q3o3, q3o4, q4o1, q4o2, q4o3, q4o4, q5o1, q5o2, q5o3, q5o4,
                q6o1, q6o2, q6o3, q6o4, q7o1, q7o2, q7o3, q7o4, q8o1, q8o2, q8o3, q9o1, q9o2, q9o3, q9o4, q9o5, q10o1, q10o2, q10o3);
            await context.SaveChangesAsync();
        }
        {
            var t = new Ticket { TicketText = "Билет № 4", Examen = ea1 };
            context.Tickets.Add(t);
            await context.SaveChangesAsync();
            var q1 = new Question { QuestionText = "В каких документах устанавливаются формы оценки соответствия обязательным требованиям к техническим устройствам, применяемым на опасном производственном объекте?", Ticket = t };
            var q2 = new Question { QuestionText = "В каком виде допускается представлять сведения об организации производственного контроля за соблюдением требований промышленной безопасности в Ростехнадзор?", Ticket = t };
            var q3 = new Question { QuestionText = "В отношении каких объектов предусмотрена разработка планов мероприятий по локализации и ликвидации последствий аварий на опасных производственных объектах?", Ticket = t };
            var q4 = new Question { QuestionText = "Какая из перечисленных задач не относится к задачам производственного контроля за соблюдением требований промышленной безопасности на опасном производственном объекте?", Ticket = t };
            var q5 = new Question { QuestionText = "Какие сроки действия планов мероприятий по локализации и ликвидации последствий аварий установлены для объектов III класса опасности (за исключением объектов, на которых ведутся горные работы)?", Ticket = t };
            var q6 = new Question { QuestionText = "Какой минимальный срок действия лицензии установлен Федеральным законом от 04.05.2011 № 99-ФЗ «О лицензировании отдельных видов деятельности»?", Ticket = t };
            var q7 = new Question { QuestionText = "Кто ведет реестр заключений экспертизы промышленной безопасности?", Ticket = t };
            var q8 = new Question { QuestionText = "Кто является страхователями гражданской ответственности за причинение вреда в результате аварии на опасном производственном объекте?", Ticket = t };
            var q9 = new Question { QuestionText = "С какой периодичностью организация, эксплуатирующая опасные производственные объекты, должна направлять информацию об инцидентах, происшедших на опасных производственных объектах, в территориальный орган Ростехнадзора?", Ticket = t };
            var q10 = new Question { QuestionText = "Что обязан сделать лицензиат, если он планирует выполнять работы (оказывать услуги), составляющие лицензируемую деятельность, и не указанные в лицензии?", Ticket = t };
            context.Questions.AddRange(q1, q2, q3, q4, q5, q6, q7, q8, q9, q10);
            await context.SaveChangesAsync();
            var q1o1 = new QuestionOption { OptionText = "В технических регламентах.", Question = q1, IsCorrect = true };
            var q1o2 = new QuestionOption { OptionText = "В федеральных нормах и правилах в области промышленной безопасности.", Question = q1 };
            var q1o3 = new QuestionOption { OptionText = "В соответствующих нормативных правовых актах, утверждаемых Правительством Российской Федерации.", Question = q1 };
            var q1o4 = new QuestionOption { OptionText = "В Федеральном законе от 21.07.1997 № 116-ФЗ «О промышленной безопасности опасных производственных объектов».", Question = q1 };
            var q2o1 = new QuestionOption { OptionText = "В письменной форме либо в форме электронного документа, подписанного усиленной квалифицированной электронной подписью.", Question = q2, IsCorrect = true };
            var q2o2 = new QuestionOption { OptionText = "В виде электронного документа в формате .pdf.", Question = q2 };
            var q2o3 = new QuestionOption { OptionText = "Обязательно на бумажном носителе.", Question = q2 };
            var q2o4 = new QuestionOption { OptionText = "В формате JSON.", Question = q2 };
            var q3o1 = new QuestionOption { OptionText = "Всех опасных производственных объектов.", Question = q3 };
            var q3o2 = new QuestionOption { OptionText = "Опасных производственных объектов I, II и III классов опасности, предусмотренных пп. 1, 4, 5 и 6 приложения 1 к Федеральному закону от 21.07.1997 № 116-ФЗ «О промышленной безопасности опасных производственных объектов».", Question = q3, IsCorrect = true };
            var q3o3 = new QuestionOption { OptionText = "Опасных производственных объектов I и II классов опасности.", Question = q3 };
            var q4o1 = new QuestionOption { OptionText = "Анализ состояния промышленной безопасности опасных производственных объектов.", Question = q4 };
            var q4o2 = new QuestionOption { OptionText = "Декларирование соответствия условий труда государственным нормативным требованиям охраны труда.", Question = q4, IsCorrect = true };
            var q4o3 = new QuestionOption { OptionText = "Контроль за своевременным проведением необходимых испытаний и технических освидетельствований технических устройств, применяемых на опасных производственных объектах, ремонта и поверки контрольных средств измерений.", Question = q4 };
            var q4o4 = new QuestionOption { OptionText = "Координация работ, направленных на предупреждение аварий на опасных производственных объектах.", Question = q4 };
            var q5o1 = new QuestionOption { OptionText = "5 лет.", Question = q5, IsCorrect = true };
            var q5o2 = new QuestionOption { OptionText = "3 года.", Question = q5 };
            var q5o3 = new QuestionOption { OptionText = "2 года.", Question = q5 };
            var q5o4 = new QuestionOption { OptionText = "1 год.", Question = q5 };
            var q6o1 = new QuestionOption { OptionText = "5 лет.", Question = q6 };
            var q6o2 = new QuestionOption { OptionText = "3 года.", Question = q6 };
            var q6o3 = new QuestionOption { OptionText = "1 год.", Question = q6 };
            var q6o4 = new QuestionOption { OptionText = "Лицензия действует бессрочно.", Question = q6, IsCorrect = true };
            var q7o1 = new QuestionOption { OptionText = "Федеральное агентство по техническому регулированию и метрологии.", Question = q7 };
            var q7o2 = new QuestionOption { OptionText = "Федеральное автономное учреждение «Главное управление государственной экспертизы».", Question = q7 };
            var q7o3 = new QuestionOption { OptionText = "Федеральная служба по аккредитации.", Question = q7 };
            var q7o4 = new QuestionOption { OptionText = "Ростехнадзор и его территориальные органы.", Question = q7, IsCorrect = true };
            var q8o1 = new QuestionOption { OptionText = "Владельцы опасных производственных объектов (юридические лица или индивидуальные предприниматели), заключившие договор обязательного страхования гражданской ответственности за причинение вреда потерпевшим в результате аварии на опасном объекте.", Question = q8, IsCorrect = true };
            var q8o2 = new QuestionOption { OptionText = "Владельцы опасных производственных объектов, за исключением индивидуальных предпринимателей, заключившие договор обязательного страхования гражданской ответственности за причинение вреда потерпевшим в результате аварии на опасном объекте.", Question = q8 };
            var q8o3 = new QuestionOption { OptionText = "Юридические лица и физические лица, заключившие со страховщиками договоры страхования.", Question = q8 };
            var q9o1 = new QuestionOption { OptionText = "Информация об инцидентах не сообщается в Ростехнадзор и его территориальные органы", Question = q9 };
            var q9o2 = new QuestionOption { OptionText = "Ежеквартально", Question = q9, IsCorrect = true };
            var q9o3 = new QuestionOption { OptionText = "Информация направляется раз в три месяца при наличии инцидентов", Question = q9 };
            var q9o4 = new QuestionOption { OptionText = "Ежегодно, независимо от того, были инциденты или нет", Question = q9 };
            var q10o1 = new QuestionOption { OptionText = "Подать заявление в лицензирующий орган о переоформлении лицензии.", Question = q10, IsCorrect = true };
            var q10o2 = new QuestionOption { OptionText = "Подать заявление в лицензирующий орган о выдаче новый лицензии.", Question = q10 };
            var q10o3 = new QuestionOption { OptionText = "Направить в лицензирующий орган уведомление о своих намерениях.", Question = q10 };
            var q10o4 = new QuestionOption { OptionText = "Направить в лицензирующий орган информацию о наличии экспертов, аттестованных в областях аттестации, соответствующих вновь заявляемым работам (услугам).", Question = q10 };
            context.QuestionOptions.AddRange(q1o1, q1o2, q1o3, q1o4, q2o1, q2o2, q2o3, q2o4, q3o1, q3o2, q3o3, q4o1, q4o2, q4o3, q4o4, q5o1, q5o2, q5o3, q5o4,
                q6o1, q6o2, q6o3, q6o4, q7o1, q7o2, q7o3, q7o4, q8o1, q8o2, q8o3, q9o1, q9o2, q9o3, q9o4, q10o1, q10o2, q10o3, q10o4);
            await context.SaveChangesAsync();
        }

        {
            var er1 = new ExamenResult
            {
                DateTime = DateTime.Today.AddHours(8),
                SurName = "Иванов",
                FirstName = "Иван",
                Patronymic = "Иванович",
                Birthday = DateTime.Today.AddYears(-20),
                ExamenName = "А.1 (ПБ 115.15) Основы промышленной безопасности",
                TicketText = "Билет № 1",
                ValidCount = 9,
                ErrorCount = 1,
                Count = 10,
                SpanRunningExam = new TimeSpan(0, 6, 0),
                CountMinutesToExam = 10,
                MaxErrorsCount = 2,
            };
            context.ExamenResults.AddRange(er1);
            await context.SaveChangesAsync();
            var q1 = new ResultQuestion { QuestionText = "В виде каких файлов должны формироваться электронные документы при подготовке отчета о производственном контроле?", ExamenResult = er1, Correct = 1, Selected = 1 };
            var q2 = new ResultQuestion { QuestionText = "В какой срок со дня поступления требования страхователя об изменении условий договора обязательного страхования в связи с уменьшением страхового риска, включая уменьшение размера страховой премии, страховщик обязан рассмотреть такое требование?", ExamenResult = er1, Correct = 4, Selected = 4 };
            var q3 = new ResultQuestion { QuestionText = "В каком случае эксплуатирующая организация вправе разрабатывать единый план мероприятий по локализации и ликвидации последствий аварий на опасных производственных объектах на несколько опасных объектов?", ExamenResult = er1, Correct = 3, Selected = 2 };
            var q4 = new ResultQuestion { QuestionText = "Как называется один из видов деятельности в области промышленной безопасности, подлежащий лицензированию в соответствии с Федеральным законом от 04.05.2011 № 99-ФЗ «О лицензировании отдельных видов деятельности»?", ExamenResult = er1, Correct = 2, Selected = 2 };
            var q5 = new ResultQuestion { QuestionText = "Какие сведения отражаются в заключении экспертизы промышленной безопасности по результатам экспертизы технического устройства?", ExamenResult = er1, Correct = 3, Selected = 3 };
            var q6 = new ResultQuestion { QuestionText = "Какое определение соответствует понятию «авария», изложенному в Федеральном законе от 21.07.1997 № 116-ФЗ «О промышленной безопасности опасных производственных объектов»?", ExamenResult = er1, Correct = 6, Selected = 6 };
            var q7 = new ResultQuestion { QuestionText = "Когда план мероприятий по локализации и ликвидации последствий аварий считается принятым?", ExamenResult = er1, Selected = 1, Correct = 1 };
            var q8 = new ResultQuestion { QuestionText = "Кто утверждает декларацию промышленной безопасности?", ExamenResult = er1, Correct = 3, Selected = 3 };
            var q9 = new ResultQuestion { QuestionText = "При каком условии событие признается страховым случаем?", ExamenResult = er1, Correct = 3, Selected = 3 };
            var q10 = new ResultQuestion { QuestionText = "Что из указанного относится к обязанностям организации в области промышленной безопасности в соответствии с Федеральным законом от 21.07.1997 № 116-ФЗ «О промышленной безопасности опасных производственных объектов»?", ExamenResult = er1, Selected = 4, Correct = 4 };
            context.ResultQuestions.AddRange(q1, q2, q3, q4, q5, q6, q7, q8, q9, q10);
            await context.SaveChangesAsync();
            var q1o1 = new ResultQuestionOption { OptionText = "В формате XML.", ResultQuestion = q1, IsCorrect = true };
            var q1o2 = new ResultQuestionOption { OptionText = "В формате DIF.", ResultQuestion = q1 };
            var q1o3 = new ResultQuestionOption { OptionText = "В форматах JPEG, TIFF, BMP, PDF.", ResultQuestion = q1 };
            var q1o4 = new ResultQuestionOption { OptionText = "В формате JSON.", ResultQuestion = q1 };
            var q2o1 = new ResultQuestionOption { OptionText = "Все ответы неверны.", ResultQuestion = q2 };
            var q2o2 = new ResultQuestionOption { OptionText = "5 рабочих дней.", ResultQuestion = q2 };
            var q2o3 = new ResultQuestionOption { OptionText = "10 рабочих дней.", ResultQuestion = q2 };
            var q2o4 = new ResultQuestionOption { OptionText = "30 рабочих дней.", ResultQuestion = q2, IsCorrect = true };
            var q2o5 = new ResultQuestionOption { OptionText = "20 рабочих дней.", ResultQuestion = q2 };
            var q3o1 = new ResultQuestionOption { OptionText = "В случае если объекты зарегистрированы в государственном реестре опасных производственных объектов.", ResultQuestion = q3 };
            var q3o2 = new ResultQuestionOption { OptionText = "План мероприятий разрабатывается на каждый опасный объект отдельно.", ResultQuestion = q3 };
            var q3o3 = new ResultQuestionOption { OptionText = "В случае если 2 и более объектов, эксплуатируемых одной организацией, расположены на одном земельном участке или на смежных земельных участках.", ResultQuestion = q3, IsCorrect = true };
            var q3o4 = new ResultQuestionOption { OptionText = "В случае если это регламентировано внутренней документацией организации.", ResultQuestion = q3 };
            var q3o5 = new ResultQuestionOption { OptionText = "Все ответы неверны.", ResultQuestion = q3 };
            var q4o1 = new ResultQuestionOption { OptionText = "Эксплуатация взрывопожароопасных производственных объектов.", ResultQuestion = q4 };
            var q4o2 = new ResultQuestionOption { OptionText = "Эксплуатация взрывопожароопасных и химически опасных производственных объектов I, II и III классов опасности.", ResultQuestion = q4, IsCorrect = true };
            var q4o3 = new ResultQuestionOption { OptionText = "Эксплуатация взрывопожароопасных и химически опасных производственных объектов всех классов опасности.", ResultQuestion = q4 };
            var q4o4 = new ResultQuestionOption { OptionText = "Эксплуатация химически опасных производственных объектов.", ResultQuestion = q4 };
            var q5o1 = new ResultQuestionOption { OptionText = "Выводы о правильности и достоверности выполненных расчетов по анализу риска, а также полноты учета факторов, влияющих на конечные результаты.", ResultQuestion = q5 };
            var q5o2 = new ResultQuestionOption { OptionText = "Обоснованность применяемых физико-математических моделей и использованных методов расчета последствий аварии и показателей риска.", ResultQuestion = q5 };
            var q5o3 = new ResultQuestionOption { OptionText = "Расчетные и аналитические процедуры оценки и прогнозирования технического состояния объекта экспертизы, включающие определение остаточного ресурса (срока службы).", ResultQuestion = q5, IsCorrect = true };
            var q5o4 = new ResultQuestionOption { OptionText = "Выводы о достаточности мер предотвращения проникновения на опасный производственный объект посторонних лиц.", ResultQuestion = q5 };
            var q6o1 = new ResultQuestionOption { OptionText = "Отказ или повреждение технических устройств, применяемых на опасном производственном объекте, отклонение от установленного режима технологического процесса.", ResultQuestion = q6 };
            var q6o2 = new ResultQuestionOption { OptionText = "Разрушение сооружений и (или) технических устройств, применяемых на опасном производственном объекте, неконтролируемые взрыв и (или) выброс опасных веществ.", ResultQuestion = q6, IsCorrect = true };
            var q6o3 = new ResultQuestionOption { OptionText = "Нарушение целостности или полное разрушение сооружений и технических устройств опасного производственного объекта при отсутствии взрыва либо выброса опасных веществ.", ResultQuestion = q6 };
            var q6o4 = new ResultQuestionOption { OptionText = "Контролируемое и (или) неконтролируемое горение, а также взрыв опасного производственного объекта.", ResultQuestion = q6 };
            var q7o1 = new ResultQuestionOption { OptionText = "После утверждения руководителями (заместителями руководителей) организаций, эксплуатирующих объекты, либо руководителями обособленных подразделений юридических лиц (в случаях, предусмотренных положениями о таких обособленных подразделениях) и согласования руководителями (заместителями руководителей, в должностные обязанности которых входит согласование планов мероприятий) профессиональных аварийно-спасательных служб или профессиональных аварийно-спасательных формирований, которые привлекаются для локализации и ликвидации последствий аварий на объекте.", ResultQuestion = q7, IsCorrect = true };
            var q7o2 = new ResultQuestionOption { OptionText = "После утверждения руководителем организации, эксплуатирующей опасные производственные объекты, или руководителями обособленных подразделений и согласования с органами Ростехнадзора.", ResultQuestion = q7 };
            var q7o3 = new ResultQuestionOption { OptionText = "После утверждения руководителем организации, эксплуатирующей опасные производственные объекты, или руководителями обособленных подразделений.", ResultQuestion = q7 };
            var q8o1 = new ResultQuestionOption { OptionText = "Руководитель эксплуатирующей организации совместно с территориальным органом Ростехнадзора.", ResultQuestion = q8 };
            var q8o2 = new ResultQuestionOption { OptionText = "Руководитель территориального органа федерального органа исполнительной власти в области промышленной безопасности или его заместители.", ResultQuestion = q8 };
            var q8o3 = new ResultQuestionOption { OptionText = "Руководитель организации, эксплуатирующей опасный производственный объект.", ResultQuestion = q8, IsCorrect = true };
            var q8o4 = new ResultQuestionOption { OptionText = "Руководитель экспертной организации, выполнившей экспертизу декларации промышленной безопасности.", ResultQuestion = q8 };
            var q9o1 = new ResultQuestionOption { OptionText = "Если в результате аварии на опасном объекте после окончания действия договора страхования причинен вред нескольким потерпевшим.", ResultQuestion = q9 };
            var q9o2 = new ResultQuestionOption { OptionText = "Если вред, причиненный в период действия договора страхования, является результатом последствий или продолжающегося воздействия аварии, произошедшей до заключения договора обязательного страхования.", ResultQuestion = q9 };
            var q9o3 = new ResultQuestionOption { OptionText = "Если причинен вред потерпевшим, явившийся результатом последствий воздействия аварии, произошедшей в период действия договора обязательного страхования, которое влечет за собой обязанность страховщика произвести страховую выплату потерпевшим.", ResultQuestion = q9, IsCorrect = true };
            var q10o1 = new ResultQuestionOption { OptionText = "Декларирование соответствия условий труда государственным нормативным требованиям охраны труда.", ResultQuestion = q10 };
            var q10o2 = new ResultQuestionOption { OptionText = "Обеспечение работников опасного производственного объекта средствами индивидуальной защиты.", ResultQuestion = q10 };
            var q10o3 = new ResultQuestionOption { OptionText = "Разработка локальных нормативных документов по охране труда.", ResultQuestion = q10 };
            var q10o4 = new ResultQuestionOption { OptionText = "Наличие на опасном производственном объекте нормативных правовых актов, устанавливающих требования промышленной безопасности, а также правил ведения работ на опасном производственном объекте.", ResultQuestion = q10, IsCorrect = true };
            context.ResultQuestionOptions.AddRange(q1o1, q1o2, q1o3, q1o4, q2o1, q2o2, q2o3, q2o4, q2o5, q3o1, q3o2, q3o3, q3o4, q3o5, q4o1, q4o2, q4o3, q4o4, q5o1, q5o2, q5o3, q5o4,
                q6o1, q6o2, q6o3, q6o4, q7o1, q7o2, q7o3, q8o1, q8o2, q8o3, q8o4, q9o1, q9o2, q9o3, q10o1, q10o2, q10o3, q10o4);
            await context.SaveChangesAsync();
        }



        //var q1 = new Question { QuestionText = "", Ticket = t };
        //var q2 = new Question { QuestionText = "", Ticket = t };
        //var q3 = new Question { QuestionText = "", Ticket = t };
        //var q4 = new Question { QuestionText = "", Ticket = t };
        //var q5 = new Question { QuestionText = "", Ticket = t };
        //var q6 = new Question { QuestionText = "", Ticket = t };
        //var q7 = new Question { QuestionText = "", Ticket = t };
        //var q8 = new Question { QuestionText = "", Ticket = t };
        //var q9 = new Question { QuestionText = "", Ticket = t };
        //var q10 = new Question { QuestionText = "", Ticket = t };
        //context.Questions.AddRange(q1, q2, q3, q4, q5, q6, q7, q8, q9, q10);
        //await context.SaveChangesAsync();


        logger.LogInformation("Complete writing test data to database ExamTicketsContext ...");
    }
}

