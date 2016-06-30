using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EDN;


namespace EDNParserTest2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("EDN Parser Testing...");

            try
            {
                TestSampleCustomHandler();
                //system("pause");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadKey();
        }

        public static void TestSampleCustomHandler()
        {
            //var r1 = EDNReader.EDNReaderFuncs.readString(\"[1 2 {:a 3 1.2 \\c {1 2} 4}]\", new SampleCustomHandler());
            //EdnNode someMap = read(\"{:some :map :with [a vector of symbols]}\");

            //EdnNode someMap = Util.read(\"{:some :map :with [a vector of symbols]}\");
            //EdnNode someMap = Util.read(\"[1 2 {:a 3 1.2 \\c {1 2} 4}]\");
            //EdnNode someMap = Util.read(\"[1 2 {:a 3 1.2 \\c {1 2} 4}]\");
            //EdnNode someMap = Util.read(\"{\\"tableid\\"[\\"sb\\"50]}\");
            //EdnNode someMap = Util.read(\"{\\"cmd\\"\\"STableList\\"\\"data\\"{\\"tables\\"[{\\"tableId\\"\\"e39d509f-dd14-4000-9936-988ad51d3532\\"\\"gameName\\"\\"texas\\"\\"sb\\"50\\"bb\\"100\\"turnListNum\\"0\\"waitListNum\\"0\\"standListNum\\"0\\"emptySeatIndexes\\"[0 1 2 3 4 ]}]}}\");
            //EdnNode someMap = Util.read(\"{\\"a\\"50\\"b\\"100}\");
            //EdnNode someMap = Util.read("\"a\"[0 1 2 3 4]");
            //EdnNode someMap = Util.read("{\"a\" 50 \"b\" 100 }");
            EdnNode someMap = Util.read("{\"eventJackpot\"[1.00100326E8 5005978.0 2003956.0]\"jackpotResult\"[[100000 100000 140000 160000 200000 200000 300000 400000 400000 0][0 1 2 3 4 5 6 7 8 9]]}");

            //EdnNode someMap = Util.read(\"{\\"cmd\\"\\"SStageEnd\\"\\"data\\"{\\"tableId\\"\\"uv6d8h7jf0vn\\"\\"highAward\\"{\\"overage\\"[nil nil]\\"high\\"[\\"20,[Akoz1]\\"]\\"low\\"nil}\\"svrDate\\"\\"20150327190717\\"}}\");
            //EdnNode someMap = Util.read(\"{\\"cmd\\"\\"SStageEnd\\"\\"data\\"{\\"tableId\\"\\"uv6d8h7jf0vn\\"\\"highAward\\"{\\"overage\\"[nil nil]\\"high\\"[20,[Akoz1],30,[Akoz2]]\\"low\\"nil}\\"svrDate\\"\\"20150327190717\\"}}\");
            //EdnNode someMap = Util.read(\"{\\"cmd\\"\\"SStageEnd\\"\\"data\\"{\\"overage\\"[5, TestID26]\\"high\\"[10,[TestID26]]\\"low\\"[]\\"svrDate\\"20150327200546\\"\\"tableId\\"\\"uv94jr77yyjo\\"}}\");
            //EdnNode someMap = Util.read(\"{\\"cmd\\"\\"SStageInfo\\"\\"data\\"{\\"tableId\\"\\"1h7o85gl37vw8\\"\\"dealerSeatIndex\\"0\\"turnUsers\\"[{\\"seatIndex\\"0\\"token\\"\\"TestID23\\"\\"playMoney\\"990\\"blind\\"{\\"B\\"10}}{\\"seatIndex\\"1\\"token\\"\\"TestID24\\"\\"playMoney\\"995\\"blind\\"{\\"S\\"5}}]\\"svrDate\\"\\"20150331131344\\"}}\");
            //EdnNode someMap = Util.read(\"{\\"cmd\\"\\"SStageInfo\\"\\"data\\"{\\"tableId\\"\\"ubp5vnlyk676\\"\\"dealerSeatIndex\\"2\\"turnUsers\\"[{\\"seatIndex\\"3\\"token\\"\\"Akoz1\\"\\"playMoney\\"990\\"blind\\"{\\"P\\"10}}{\\"seatIndex\\"4\\"token\\"\\"Akoz3\\"\\"playMoney\\"990\\"blind\\"{\\"P\\"10}}{\\"seatIndex\\"2\\"token\\"\\"Akoz5\\"\\"playMoney\\"990\\"blind\\"{}}{\\"seatIndex\\"1\\"token\\"\\"Akoz13\\"\\"playMoney\\"1020\\"blind\\"{}}]\\"svrDate\\"\\"20150407110152\\"}}\");
            //EdnNode someMap = Util.read("{\"cmd\"\"STableList\"\"data\"{\"tables\"[{\"standListNum\"4\"tableId\"\"1hrm0lcef9j5e\"\"sb\"5\"turnListNum\"0\"emptySeatIndexes\"[0 1 3 4]\"waitListNum\"1\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"2\"tableId\"\"1hbaymu4d1jdw\"\"sb\"5\"turnListNum\"2\"emptySeatIndexes\"[1 2]\"waitListNum\"1\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"2\"tableId\"\"1h7lgdmquxtgj\"\"sb\"5\"turnListNum\"3\"emptySeatIndexes\"[]\"waitListNum\"2\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"2\"tableId\"\"1hrlz19z6u1q0\"\"sb\"5\"turnListNum\"3\"emptySeatIndexes\"[0 1]\"waitListNum\"0\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"0\"tableId\"\"1hrbeyon092yf\"\"sb\"5\"turnListNum\"3\"emptySeatIndexes\"[]\"waitListNum\"2\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"2\"tableId\"\"t84lopujep85\"\"sb\"5\"turnListNum\"3\"emptySeatIndexes\"[1 4]\"waitListNum\"0\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"1\"tableId\"\"1iuvibe5uiz3q\"\"sb\"5\"turnListNum\"3\"emptySeatIndexes\"[0]\"waitListNum\"1\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"0\"tableId\"\"r17sdm2fp469\"\"sb\"5\"turnListNum\"4\"emptySeatIndexes\"[]\"waitListNum\"1\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"2\"tableId\"\"1iuyqxdy7b5nn\"\"sb\"5\"turnListNum\"2\"emptySeatIndexes\"[2 3]\"waitListNum\"1\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"1\"tableId\"\"s0xamfr477tt\"\"sb\"5\"turnListNum\"2\"emptySeatIndexes\"[1]\"waitListNum\"2\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"2\"tableId\"\"1ibhlio75git4\"\"sb\"5\"turnListNum\"3\"emptySeatIndexes\"[2 4]\"waitListNum\"0\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"2\"tableId\"\"t89m58yhpilf\"\"sb\"5\"turnListNum\"3\"emptySeatIndexes\"[1 4]\"waitListNum\"0\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"2\"tableId\"\"qe3w179jaycj\"\"sb\"5\"turnListNum\"3\"emptySeatIndexes\"[1 4]\"waitListNum\"0\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"3\"tableId\"\"rhtwz7pemrms\"\"sb\"5\"turnListNum\"2\"emptySeatIndexes\"[0 3 4]\"waitListNum\"0\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"1\"tableId\"\"s1jhzx3nfgpw\"\"sb\"5\"turnListNum\"3\"emptySeatIndexes\"[1]\"waitListNum\"1\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"1\"tableId\"\"1hrm1rdr4m82p\"\"sb\"5\"turnListNum\"3\"emptySeatIndexes\"[2]\"waitListNum\"1\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"2\"tableId\"\"ts2g331nxxkk\"\"sb\"5\"turnListNum\"2\"emptySeatIndexes\"[1 4]\"waitListNum\"1\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"1\"tableId\"\"1ies947lc2gj4\"\"sb\"5\"turnListNum\"3\"emptySeatIndexes\"[2 3]\"waitListNum\"0\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"1\"tableId\"\"urmbnpuxmm9c\"\"sb\"5\"turnListNum\"3\"emptySeatIndexes\"[3]\"waitListNum\"1\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"2\"tableId\"\"tnz0xag8a5pf\"\"sb\"5\"turnListNum\"3\"emptySeatIndexes\"[0 1]\"waitListNum\"0\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"1\"tableId\"\"1jefhfvdb8t2q\"\"sb\"5\"turnListNum\"3\"emptySeatIndexes\"[2]\"waitListNum\"1\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"2\"tableId\"\"1iuqf4z27k4l2\"\"sb\"5\"turnListNum\"2\"emptySeatIndexes\"[1 2 3]\"waitListNum\"0\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"1\"tableId\"\"r1dbjucg86sj\"\"sb\"5\"turnListNum\"3\"emptySeatIndexes\"[0]\"waitListNum\"1\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"2\"tableId\"\"u82wh62fe3w1\"\"sb\"5\"turnListNum\"2\"emptySeatIndexes\"[1 3]\"waitListNum\"1\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"2\"tableId\"\"1jiak3ssyd65f\"\"sb\"5\"turnListNum\"2\"emptySeatIndexes\"[0 2]\"waitListNum\"1\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"2\"tableId\"\"1iyq29kitv2ut\"\"sb\"5\"turnListNum\"3\"emptySeatIndexes\"[2 4]\"waitListNum\"0\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"3\"tableId\"\"1iyezmqzks86a\"\"sb\"5\"turnListNum\"0\"emptySeatIndexes\"[0 2 3 4]\"waitListNum\"1\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"3\"tableId\"\"1iukwdj5nvb5j\"\"sb\"5\"turnListNum\"2\"emptySeatIndexes\"[2 3 4]\"waitListNum\"0\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"3\"tableId\"\"s0xa8en78p5v\"\"sb\"5\"turnListNum\"2\"emptySeatIndexes\"[0 1 2]\"waitListNum\"0\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"2\"tableId\"\"s103n5jw6sz8\"\"sb\"5\"turnListNum\"2\"emptySeatIndexes\"[1 3 4]\"waitListNum\"0\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"1\"tableId\"\"1ibh3783lrujq\"\"sb\"5\"turnListNum\"3\"emptySeatIndexes\"[3]\"waitListNum\"1\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"3\"tableId\"\"u85ngxogdmjl\"\"sb\"5\"turnListNum\"2\"emptySeatIndexes\"[1 3 4]\"waitListNum\"0\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"2\"tableId\"\"qdpyg1t5q4vn\"\"sb\"5\"turnListNum\"3\"emptySeatIndexes\"[2 3]\"waitListNum\"0\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"1\"tableId\"\"t4yze3f5ukvr\"\"sb\"5\"turnListNum\"3\"emptySeatIndexes\"[2 4]\"waitListNum\"0\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"2\"tableId\"\"t4a097nnli7a\"\"sb\"5\"turnListNum\"2\"emptySeatIndexes\"[2 3]\"waitListNum\"1\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"1\"tableId\"\"qxw3tust5i5y\"\"sb\"5\"turnListNum\"3\"emptySeatIndexes\"[3]\"waitListNum\"1\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"3\"tableId\"\"1k253m6ivlh6e\"\"sb\"5\"turnListNum\"2\"emptySeatIndexes\"[1 2 4]\"waitListNum\"0\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"2\"tableId\"\"u7wvpqroxmjn\"\"sb\"5\"turnListNum\"2\"emptySeatIndexes\"[0 4]\"waitListNum\"1\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"2\"tableId\"\"1hbd654b9cm5x\"\"sb\"5\"turnListNum\"3\"emptySeatIndexes\"[3 4]\"waitListNum\"0\"bb\"10\"gameName\"\"texas\"}{\"standListNum\"1\"tableId\"\"1jezdsp03rqyf\"\"sb\"5\"turnListNum\"0\"emptySeatIndexes\"[0 1 2 4]\"waitListNum\"1\"bb\"10\"gameName\"\"texas\"}]\"svrDate\"\"20150410180134\"}}");


            //EdnNode someMap = Util.read(\"{:some :map :with [\"a\" vector of symbols]}\");

            //EdnNode someMap = Util.read(\"{ :cmd :Qlogin, :data {:login-token \\"soc1\\" }, :scene :main-lobby, :tk \\"\\"}\");
            //EdnNode someMap = Util.read(\"{:scene :main-lobby, :cmd :Rlogin, :err :ERR_NONE, :data {:tk \\"2a77e6c5-826c-42f3-86bb-2d01d4c0f029\\", \" + 
            //                            \":sid \\"sid-soc1\\", :friendlist {:friends [], :cur-page 1, :max-page 0}, :events [{:uid \\"c52adce5-b265-4bf2-869a-8a62a7ff3566\\", \" + 
            //                            \":id 1, :info {:money 1500000}}], :account {:invitee {}, :social-id \\"sid-soc1\\", \" +
            //                            \":user {:last-logout-time 1421913847144, :last-access-time 1421913847144, :money 0, :name \\"name-soc1\\", :avatar-url \\"\\", \" +
            //                            \":email \\"\\", :locale \\"\\", :gender \\"\\"}, :events [{:uid \\"c52adce5-b265-4bf2-869a-8a62a7ff3566\\", :id 1, :info {:money 1500000}}], :create-time 1421913847144, :game {:slotmachine {:freespin-count 0, :bet-amount 200}}}}}\");
            Console.WriteLine("----------------------------------");
            Console.WriteLine(Util.pprint(ref someMap));
            //System.Diagnostics.Debug.WriteLine(Util.pprint(ref someMap));

        }
    }
}
