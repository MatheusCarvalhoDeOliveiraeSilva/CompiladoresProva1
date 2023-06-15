//Dupla Matheus Carvalho de Oliveira e Silva e Thiago Rodrigues Leiria de Andrade. Matricula:01350925, 01390900

//Essas linhas exibem uma mensagem para o usuário digitar uma sequência de símbolos e, em seguida, lê a entrada do usuário. 
//O loop while verifica se o comprimento da entrada está vazio ou excede 10 caracteres.
//Se exceder, uma mensagem é exibida informando que a entrada não é permitida.

Console.WriteLine("Digite uma sequência de simbolos");
string inputString = "";
while (inputString.Length == 0 || inputString.Length > 10) {
    inputString = Console.ReadLine();
    if (inputString.Length > 10) {
        Console.WriteLine("Entrada Não Permitida. Verifique sua entrada:");
    }
}

//Essas linhas chamam a função Tokenize passando a entrada do usuário como argumento. 
//Em seguida, exibe os tokens encontrados, um por linha, na sequência em que aparecem.

List<string> tokens = Tokenize(inputString);
Console.WriteLine("Tokens:");
foreach (string token in tokens) {
    Console.WriteLine(token);
}


    
//Aqui, a função Tokenize é definida. Duas variáveis do tipo HashSet<char> são criadas: validChars, que contém os caracteres válidos para formar um token, e validOperators, que contém os operadores válidos. 
//Também são inicializadas uma lista de strings para armazenar os tokens encontrados e uma string de buffer para construir os tokens.Aqui, a função Tokenize é definida. 
//Duas variáveis do tipo HashSet<char> são criadas: validChars, que contém os caracteres válidos para formar um token, e validOperators, que contém os operadores válidos. 
//Também são inicializadas uma lista de strings para armazenar os tokens encontrados e uma string de buffer para construir os tokens.
    
    static List<string> Tokenize(string inputString) {
        HashSet<char> validChars = new HashSet<char>("abcdefghijklmnopqrsuvABCDEFGHIJKLMNOPQRSUV0123456789+-*/%()[]{}<>=!&|~^,$.@#?");
        HashSet<char> validOperators = new HashSet<char>("+-*/%<>=!&|~^");
        List<string> tokens = new List<string>();
        string buffer = "";

//Essa linha verifica se o primeiro caractere da entrada é um dígito e exibe uma mensagem informando que palavras iniciadas com números são reservadas pelo sistema.
  
    if (Char.IsDigit(inputString[0])) {
    Console.WriteLine("Palavras iniciadas com números são sempre palavras reservadas pelo sistema.");
}

//Esse loop percorre todos os tokens já encontrados e verifica se o primeiro caractere de cada token é um dígito. Se for, exibe uma mensagem informando que a palavra é indisponível.

        for (int i = 0; i < tokens.Count; i++) {
            string token = tokens[i];
            if (Char.IsDigit(token[0])) {
                Console.WriteLine($"A palavra '{token}' é uma palavra indisponível.");
            }
        }
//Este loop percorre cada caractere da entrada. Se o caractere for um dígito e o buffer estiver vazio, adiciona-se um token "Número" à lista de tokens. 
//Em seguida, continua-se lendo caracteres até que um não dígito seja encontrado ou o final da entrada seja atingido. 
//Quando isso acontece, o buffer é adicionado como um token à lista de tokens. Se o caractere atual não for um dígito nem um espaço em branco, o caractere é adicionado ao buffer. Se o buffer não estiver vazio e a função IsTokenWithXYZTW retornar verdadeiro para o buffer, adiciona-se um token "Expressão matemática" à lista de tokens. 
//Se o caractere atual não for um caractere válido ou um espaço em branco e o buffer estiver preenchido, o buffer é adicionado como um token à lista de tokens e é redefinido para uma string vazia.
       
        for (int i = 0; i < inputString.Length; i++) {
            char c = inputString[i];
            if (char.IsDigit(c) && buffer.Length == 0) {
                tokens.Add("Número");
                while (char.IsDigit(c) && i < inputString.Length - 1) {
                    buffer += c;
                    i++;
                    c = inputString[i];
                }
                if (char.IsDigit(c)) {
                    buffer += c;
                }
                tokens.Add(buffer);
                buffer = "";
            } else if (validChars.Contains(c)) {
                if (buffer.Length > 0 && IsTokenWithXYZTW(buffer)) {
                    tokens.Add("Expressão matemática");
                }
                buffer += c;
            } else {
                if (buffer.Length > 0) {
                    tokens.Add(buffer);
                    buffer = "";
                }
            }
        }

//Essa linha verifica se ainda há algum conteúdo no buffer após o término do loop. 
//Se houver, adiciona o buffer como um token à lista de tokens.

        if (buffer.Length > 0) {
            tokens.Add(buffer);
        }
//A função Tokenize retorna a lista de tokens encontrados. 

        return tokens;
    }
//Esta é uma função auxiliar chamada IsTokenWithXYZTW que recebe um token como argumento e verifica se ele atende a certas condições. 
//A função verifica se o token contém apenas os caracteres "xyztw" seguidos de um operador válido. 
//Se o token não atender a essas condições, a função retorna falso. 
//Caso contrário, retorna verdadeiro.

    static bool IsTokenWithXYZTW(string token) {
        HashSet<char> xyztwChars = new HashSet<char>("xyztw");
        HashSet<char> validOperators = new HashSet<char>("+-*/%<>=!&|~^");
        bool xyztwFlag = false;
        bool operatorFlag = false;
        for (int i = 0; i < token.Length; i++) {
            char c = token[i];
            if (xyztwChars.Contains(c)) {
                xyztwFlag = true;
                if (i < token.Length - 1) {
                    char nextChar = token[i + 1];
                    if (validOperators.Contains(nextChar)) {
                        operatorFlag = true;
                        i++;
                    } else {
                        return false;
                    }
                }
            } else if (char.IsDigit(c)) {
                return false;
            } else if (char.IsLetter(c)) {
                return false;
            } else if (validOperators.Contains(c)) {
                if (!xyztwFlag) {
                    return false;
                }
                if (operatorFlag) {
                    return false;
                }
                operatorFlag = true;
            } else {
                return false;
            }
        }
        return xyztwFlag && operatorFlag;
    }