from file_manager import FileManager

fileManager = FileManager("plik.txt")

text = fileManager.read_file()

text += " , dodatkowy tekst"

fileManager.update_file(text)