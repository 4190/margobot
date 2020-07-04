# trashbot
Bot zapisujący i wyliczający timery respawnów herosów. 

Dane herosów zapisywać w folderze o nazwie "heroes" a o coordach w folderze o nazwie "coords" (podfoldery obok pliku .exe bota) w plikach .json

Przykładowy plik z folderu heroes
["21.3","Atalia","09:19","12:49","21:19","https://www.margonem.pl/obrazki/npc/kob/tri_atalia.gif","3","30","12","0"]
(data ostatniego ubicia, nazwa herosa, czas ostatniego ubicia, minimal resp timer, maximal resp timer, obrazek herosa, minimalna liczba godzin do spawnu, minimalna liczba minut do spawnu, max liczba godzin do spawnu, max liczba minut do spawnu)

Przykładowy plik z folderu coords
["Amra's House (15,10)","Atalia's House (6,8)","Eerie Road (15,43)","Plundered Chapel (7,7)","Sabbath Mountains (32,48)"]
Kolejne koordynaty po , i sortowane alfabetycznie.

W świeżym pliku zapisać [""]

Nazwa pliku json w folderze heroes jest jednocześnie słowem wywołującym tego bossa z komendą (w folderze heroes nazwa normalnie, w folderze coords nazwa + "Coords" np: "ataliaCoords")













Tokeny bota ze starych commitów są zresetowane ;)
