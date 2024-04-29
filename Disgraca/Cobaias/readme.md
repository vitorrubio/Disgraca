# Disgra�a

## O que essa disgraça faz?
Ferramenta de texto para converter todos os arquivos de código com os encodings desgraçados que você não usa para a desgraça de encoding que você usa (e que seja utf-8 né?!).

## Como uso essa disgraça?
Uso:

> Disgraca `<operação (convert|list)>` `<encoding desejado>` `<diretorio>` `<extensão *.xxx>` `[<demais extensões *.xxx>]` onde *.xxx é sua extensão com wildcards e sem <> []

Exemplo: 

`disgraca convert utf-8 c:\MeuProjeto *.css *.js *.htm`

`disgraca list c:\MeuProjeto *.css *.js *.htm`

## E pra que eu faria isso?
Na desgraça de um projeto legado você vai ter misturas de arquivos em 10 encodings diferentes. Escolha um (escolha utf-8 pombas), e converta todos os arquivos para este escolhido. Esse cara usa Ude.CharsetDetector() para detectar o encoding original, assim você não precisa saber e não há perda na hora de converter para o encoding desejado. Mais ou menos o processo que o notepad++ faz. 

