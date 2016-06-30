using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EDN
{
    //String validSymbolChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ.*+!-_?$%&=:#/";
    
    public enum TokenType {
        TokenString,
        TokenAtom,
        TokenParen
    };

    public enum NodeType
    {
        EdnNil,
        EdnSymbol,
        EdnKeyword,
        EdnBool,
        EdnInt,
        EdnFloat,
        EdnString,
        EdnChar,

        EdnList,
        EdnVector,
        EdnMap,
        EdnSet,

        EdnDiscard,
        EdnTagged
    };

    public class EdnToken
    {
        public TokenType type;
        public int line;
        public String value;
    }

    public class EdnNode
    {
        public NodeType type;
        public int line;
        public String value;
        //list<EdnNode> values;
        public List<EdnNode> values;
    }

    public static class Util
    {
        //static String validSymbolChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ.*+!-_?$%&=:#/";
        static char[] validSymbolChars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 
                                                      'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 
                                                      '.', '*', '+', '!', '-', '_', '?', '$', '%', '&', '=', ':', '#', '/' };

        //by default checks if first char is in range of chars
        public static bool strRangeIn(String str, String range, int start = 0, int stop = 1) 
        {
            //String strRange = str.substr(start, stop);
            String strRange = str.Substring(start, stop);

            //return (std::strspn(strRange.c_str(), range) == strRange.length());
            return (strRange.IndexOfAny(range.ToCharArray()) == strRange.Length);
        }

        public static void createToken(TokenType type, int line, String value, ref List<EdnToken> tokens) 
        {
            EdnToken token = new EdnToken();
            token.type = type;
            token.line = line;
            token.value = value;
           
            //tokens.push_back(token);
            tokens.Add(token);
        }

        public static EdnToken shiftToken(ref List<EdnToken> tokens) 
        { 
            //EdnToken nextToken = tokens.front();
            //tokens.pop_front();

            EdnToken nextToken = tokens.First();
            tokens.RemoveAt(0);
            return nextToken;
        }

        public static EdnNode handleCollection(EdnToken token, List<EdnNode> values)
        {
            EdnNode node = new EdnNode();
            node.line = token.line;
            node.values = values;

            if (token.value == "(")
            {
                node.type = NodeType.EdnList;
            }
            else if (token.value == "[")
            {
                node.type = NodeType.EdnVector;
            }
            if (token.value == "{")
            {
                node.type = NodeType.EdnMap;
            }
            return node;
        }

        public static EdnNode handleTagged(EdnToken token, EdnNode value)
        {
            EdnNode node = new EdnNode();
            node.line = token.line;

            //String tagName = token.value.substr(1, token.value.length() - 1);
            String tagName = token.value.Substring(1, token.value.Length - 1);
             
            if (tagName == "_")
            {
                node.type = NodeType.EdnDiscard;
            }
            else if (tagName == "")
            {
                //special case where we return early as # { is a set - thus tagname is empty
                node.type = NodeType.EdnSet;
                if (value.type != NodeType.EdnMap)
                {
                    throw new Exception("Was expection a { } after hash to build set");
                }
                node.values = value.values;
                return node;
            }
            else
            {
                node.type = NodeType.EdnTagged;
            }

            if (!validSymbol(tagName))
            {
                throw new Exception("Invalid tag name");
            }

            EdnToken symToken = new EdnToken();
            symToken.type = TokenType.TokenAtom;
            symToken.line = token.line;
            symToken.value = tagName;

            List<EdnNode> values = new List<EdnNode>();
            //values.push_back(handleAtom(symToken));
            //values.push_back(value);
            values.Add(handleAtom(symToken));

            node.values = values;
            return node;
        }

        public static EdnNode readAhead(EdnToken token, ref List<EdnToken> tokens)
        {
            
            if (token.type == TokenType.TokenParen) 
            {
                EdnToken nextToken;
                List<EdnNode> L = new List<EdnNode>();
                String closeParen = "";
                if (token.value == "(") closeParen = ")";
                if (token.value == "[") closeParen = "]"; 
                if (token.value == "{") closeParen = "}";

                int idx = 0;
                while (true) 
                {
                    idx++;
                    //if (tokens.empty()) throw new Exception("unexpected end of list");
                    if (!tokens.Any())
                    {
                        Console.WriteLine("idx " + idx);
                        throw new Exception("unexpected end of list");
                    }


                    nextToken = shiftToken(ref tokens);



                    Console.WriteLine("idx " + idx + " " + nextToken.value);
                    if (nextToken.value == closeParen) 
                    {
                        return handleCollection(token, L);
                    } 
                    else 
                    {
                        L.Add(readAhead(nextToken, ref tokens));
                    }
               }
                
            } 
            else if (token.value == ")" || token.value == "]" || token.value == "}") 
            {
                throw new Exception("Unexpected " + token.value);
            } 
            else 
            {
                if (token.value.Length > 0 && token.value[0] == '#') 
                {
                    return handleTagged(token, readAhead(shiftToken(ref tokens), ref tokens));
                } 
                else 
                {
                    return handleAtom(token);
                }
            }
        }

        public static List<EdnToken> lex(string edn)
        {
            int line = 1;
            char escapeChar = '\\';
            bool escaping = false;
            bool inString = false;
            string stringContent = "";
            bool inComment = false;
            string token = "";
            string paren = "";
            List<EdnToken> tokens = new List<EdnToken>();


            //bool inNumber = false;
            //string numberContent = string.Empty;
            //char previt = ' ';
            
            Console.WriteLine(edn);
            
            //for (it = edn.begin(); it != edn.end(); ++it)
            for (int i = 0; i < edn.Length; i++ )
            {
                
                char it = edn[i];
                
                //if (*it == '\n' || *it == '\r') line++;
                if (it == '\n' || it == 'r') line++;

                //Console.WriteLine("line: {0}, char: {1}", line, edn[i]);

                //if (!inString && *it == ';' && !escaping) inComment = true;
                if (!inString && it == ';' && !escaping) inComment = true;

                //---------------------------------------------------------------------------------
                if (inComment)
                {
                    //Console.WriteLine("     inComment....");
                    //if (*it == '\n')
                    if(it == '\n')
                    {
                        inComment = false;
                        if (token != "")
                        {
                            createToken(TokenType.TokenAtom, line, token, ref tokens);
                            token = "";
                        }
                        continue;
                    }

                    //Console.WriteLine("     tokens : {0}", tokens);
                }

                //---------------------------------------------------------------------------------
                //if (*it == '"' && !escaping)
                if (it == '"' && !escaping)
                {
                    
                    // inString이 false상태이고 numberContent가 있다면...

                    //Console.WriteLine("     in !escaping ....");
                    if (inString)
                    {
                        createToken(TokenType.TokenString, line, stringContent, ref tokens);
                        inString = false;
                    }
                    else
                    {
                        // 여태 읽어온 토큰값이 있는지 체크후
                        // 아톰으로 분류해야 함. 일단 저장
                        if (!string.IsNullOrEmpty(token))
                        {
                            createToken(TokenType.TokenString, line, token, ref tokens);
                            token = "";
                        }

                        stringContent = "";
                        inString = true;
                    }

                    continue;
                }

                //---------------------------------------------------------------------------------
                if (inString)
                {
                    //Console.WriteLine("     inString ....");

                    //if (*it == escapeChar && !escaping)
                    if (it == escapeChar && !escaping)
                    {
                        escaping = true;
                        continue;
                    }

                    if (escaping)
                    {
                        escaping = false;
                        //if (*it == 't' || *it == 'n' || *it == 'f' || *it == 'r') stringContent += escapeChar;
                        if (it == 't' || it == 'n' || it == 'f' || it == 'r') stringContent += escapeChar;
                    }
                    stringContent += it;
                }
                else if (it == '(' || it == ')' || it == '[' || it == ']' || it == '{' || it == '}' || it == '\t' || it == '\n' || it == '\r' || it == ' ' || it == ',')
                {
                    //Console.WriteLine("     in ([{}]) ....");

                    if (token != "")
                    {
                        //Console.WriteLine("in         \"\" ....");
                        createToken(TokenType.TokenAtom, line, token, ref tokens);
                        token = "";
                    }

                    //if (*it == '(' || *it == ')' || *it == '[' || *it == ']' || *it == '{' || *it == '}')
                    if (it == '(' || it == ')' || it == '[' || it == ']' || it == '{' || it == '}')
                    {
                        // 여태 읽어온 토큰값이 있는지 체크후
                        // 아톰으로 분류해야 함. 일단 저장
                        if (!string.IsNullOrEmpty(token))
                        {
                            createToken(TokenType.TokenString, line, token, ref tokens);
                            token = "";
                        }

                        //Console.WriteLine("in         괄호들 ....");
                        paren = "";
                        createToken(TokenType.TokenParen, line, paren, ref tokens);
                    }
                }
                else
                {
                    //Console.WriteLine("     in escaping ....");
                    
                    // 토큰 조립단계

                    if (escaping)
                    {
                        escaping = false;
                    }
                    //else if (*it == escapeChar)
                    else if (it == escapeChar)
                    {
                        escaping = true;
                    }

                    if (token == "#_" || (token.Length == 2 && token[0] == escapeChar))
                    {
                        createToken(TokenType.TokenAtom, line, token, ref tokens);
                        token = "";
                    }

                    token += it;
                }

            }

            if (token != "")
            {
                createToken(TokenType.TokenAtom, line, token, ref tokens);
            }

            return tokens;
        }

        
        //public static void uppercase(string &str) { 
        //    std::transform(str.begin(), str.end(), str.begin(), ::toupper);
        //}

        public static bool validSymbol(String value) 
        {
            //first we uppercase the value
            //uppercase(value);
            value = value.ToUpper();

            //if (std::strspn(value.c_str(), validSymbolChars.c_str()) != value.Length)
            int result = value.IndexOfAny(validSymbolChars);
            if (result == -1)
            {
                return false;
            }

            
    
            //if the value starts with a number that is not ok
            if (strRangeIn(value, "0123456789"))
                return false;

            //first char can not start with : # or / - but / by itself is valid
            //if (strRangeIn(value, ":#/") && !(value.length() == 1 && value[0] == '/'))
            if (strRangeIn(value, ":#/") && !(value.Length == 1 && value[0] == '/'))
                return false;

            //if the first car is - + or . then the next char must NOT be numeric, by by themselves they are valid
            //if (strRangeIn(value, "-+.") && value.length() > 1 && strRangeIn(value, "0123456789", 1))
            if (strRangeIn(value, "-+.") && value.Length > 1 && strRangeIn(value, "0123456789", 1))
                return false;

            //if (std::count(value.begin(), value.end(), '/') > 1)
            if (value.Count(f => f == '/') > 1)
                return false;

            return true;
        }

        public static bool validKeyword(String value)
        {
            //return (value[0] == ':' && validSymbol(value.substr(1, value.length() - 1)));
            return (value[0] == ':' && validSymbol(value.Substring(1, value.Length-1)));
        }

        public static bool validNil(String value)
        {
            return (value == "nil");
        }

        public static bool validBool(String value)
        {
            return (value == "true" || value == "false");
        }

        public static bool validInt(String value, bool allowSign = true) 
        {
            //if we have a positive or negative symbol that is ok but remove it for testing
            //if (strRangeIn(value, "-+") && value.length() > 1 && allowSign) 
                //value = value.substr(1, value.length() - 1);
            if (strRangeIn(value, "-+") && value.Length > 1 && allowSign)
                value = value.Substring(1, value.Length - 1); 

            //if string ends with N or M that is ok, but remove it for testing
            //if (strRangeIn(value, "NM", value.length() - 1, 1))
                //value = value.substr(0, value.length() - 2); 
            if(strRangeIn(value, "NM", value.Length - 1, 1))
                value = value.Substring(0, value.Length - 2);

            //String anyOf = "0123456789";
            //if (std::strspn(value.c_str(), "0123456789") != value.length())

            long num = 0;
            bool canConvert = long.TryParse(value, out num);

            return canConvert;
        }

        public static bool validFloat(String value)
        {

            //uppercase(value);
            value = value.ToUpper();

            string front;
            string back;
            int epos;
            //int periodPos = value.find_first_of('.');
            int periodPos = value.IndexOf('.');
            if (periodPos >= 0)
            {
                front = value.Substring(0, periodPos);
                back = value.Substring(periodPos + 1);
            }
            else
            {
                front = "";
                back = value;
            }

            
            if (front == "" || validInt(front))
            {
                
                //epos = back.find_first_of('E');
                epos = back.IndexOf('E');
                if (epos > -1)
                {
                    //ends with E which is invalid
                    if ((uint)epos == back.Length - 1) 
                        return false;

                    //both the decimal and exponent should be valid - do not allow + or - on dec (pass false as arg to validInt)
                    if (!validInt(back.Substring(0, epos), false) || !validInt(back.Substring(epos + 1))) return false;
                }
                else
                {
                    //if back ends with M remove for validation
                    //if (strRangeIn(back, "M", back.length() - 1, 1))
                        //back = back.substr(0, back.length() - 1);
                    if (strRangeIn(back, "M", back.Length - 1, 1))
                        back = back.Substring(0, back.Length - 1);

                    if (!validInt(back, false)) 
                        return false;
                }
                
                return true;
            }
            
            return false;
        }

        public static bool validChar(string value)
        {
            //return (value.at(0) == '\\' && value.length() == 2);
            return (value[0] == '\\' && value.Length == 2);
        }

        public static EdnNode handleAtom(EdnToken token)
        {
            EdnNode node = new EdnNode();
            node.line = token.line;
            node.value = token.value;

            if (validNil(token.value))
                node.type = NodeType.EdnNil;
            else if (token.type == TokenType.TokenString)
                node.type = NodeType.EdnString;
            else if (validChar(token.value))
                node.type = NodeType.EdnChar;
            else if (validBool(token.value))
                node.type = NodeType.EdnBool;
            else if (validInt(token.value))
                node.type = NodeType.EdnInt;
            else if (validFloat(token.value))
                node.type = NodeType.EdnFloat;
            else if (validKeyword(token.value))
                node.type = NodeType.EdnKeyword;
            else if (validSymbol(token.value))
                node.type = NodeType.EdnSymbol;
            else
                throw new Exception("Could not parse atom");

            return node;
        }

        public static string escapeQuotes(string before) 
        {
            String after = "";
            //after.reserve(before.length() + 4); 
                
            //for (string::size_type i = 0; i < before.length(); ++i) 
            for (int i = 0; i < before.Length; ++i )
            {
                switch (before[i])
                {
                    case '"':
                    case '\\':
                        after += '\\';
                        break;
                    default:
                        after += before[i];
                        break;
                }
            }

            return after;
        }

        public static string pprint(ref EdnNode node, int indent = 1) 
        {
            string prefix = "";
            if (indent != 0) {
                //prefix.insert(0, indent, ' ');
                prefix.Insert(0, " ");
            }

            string output = "";
            if (node.type == NodeType.EdnList || node.type == NodeType.EdnSet || node.type == NodeType.EdnVector || node.type == NodeType.EdnMap) 
            { 
                string vals = "";
                //for (list<EdnNode>::iterator it=node.values.begin(); it != node.values.end(); ++it) 
                //foreach(EdnNode it in node.values)
                for (int i = 0; i < node.values.Count; i++ )
                {
                    if (vals.Length > 0) vals += prefix;

                    EdnNode it = node.values[i];
                    vals += pprint(ref it, indent + 1);
                    if (node.type == NodeType.EdnMap)
                    {
                        ++i;
                        it = node.values[i];
                        vals += " " + pprint(ref it, 1);
                        
                        //++it;
                        //vals += " " + pprint(*it, 1);
                    }

                    //Console.WriteLine("현재요소카운트:{0}, 마지막요소카운트:{1}, 거리:{2}", i, node.values.Count, node.values.Count-i);
                    
                    //if (std::distance(it, node.values.end()) != 1) vals += "\n";
                    if ((node.values.Count - i) != 1) vals += "\n";
                }

                if (node.type == NodeType.EdnList) output = "(" + vals + ")";
                else if (node.type == NodeType.EdnVector) output = "[" + vals + "]";
                else if (node.type == NodeType.EdnMap) output = "{" + vals + "}"; 
                else if (node.type == NodeType.EdnSet) output = "#{" + vals + "}";
     
                //#ifdef DEBUG
                //    return "<" + typeToString(node.type) + " " + output + ">"; 
                //#endif 
            } 
            else if (node.type == NodeType.EdnTagged) 
            {
                //output = "#" + pprint(node.values.front()) + " " + pprint(node.values.back());
                EdnNode first = node.values.First();
                EdnNode last = node.values.Last();
                output = "#" + pprint(ref first) + " " + pprint(ref last);

                //#ifdef DEBUG
                //    return "<EdnTagged " + output + ">";
                //#endif
            } 
            else if (node.type == NodeType.EdnString) 
            {
                output = "\"" + escapeQuotes(node.value) + "\"";
                
                //#ifdef DEBUG
                //    return "<EdnString " + output + ">";
                //#endif
            } 
            else 
            {
                //#ifdef DEBUG
                //    return "<" + typeToString(node.type) + " " + node.value + ">";
                //#endif

                output = node.value;
            }
            return output;
        }


        public static EdnNode read(string edn)
        {
            List<EdnToken> tokens = lex(edn);
            Console.WriteLine("----------------------------------");
            Console.WriteLine("tokens size:{0}", tokens.Count);
            
            foreach(EdnToken it in tokens)
            {
                Console.WriteLine("Line {0}: {1}, {2}", it.line, it.type, it.value);
            }
            

            if (tokens.Count == 0)
            {
                //throw "No parsable tokens found in string";
                throw new Exception("No parsable tokens found in string");
            }

            return readAhead(shiftToken(ref tokens), ref tokens);
            //return null;
        }
    }
    
}
