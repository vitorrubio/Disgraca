# Disgra�a

## O que essa disgra�a faz?
Ferramenta de texto para converter todos os arquivos de c�digo com os encodings desgra�ados que voc� n�o usa para a desgra�a de encoding que voc� usa (e que seja utf-8 n�?!).

## Como uso essa disgra�a?
Uso: disgraca <encoding> <diretorio> <extencoes>. Exemplo: disgraca utf-8 c:\MeuProjeto *.css *.js *.htm

## E pra que eu faria isso?
Na desgra�a de um projeto legado voc� vai ter misturas de arquivos em 10 encodings diferentes. Escolha um (escolha utf-8 pombas), e converta todos os arquivos para este escolhido. Esse cara usa Ude.CharsetDetector() para detectar o encoding original, assim voc� n�o precisa saber e n�o h� perda na hora de converter para o encoding desejado. Mais ou menos o processo que o notepad++ faz. 

