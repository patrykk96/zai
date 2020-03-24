class FileManager:
    def __init__(self, filename):
        self.filename = filename

    def read_file(self):
        file = open(self.filename)
        data = file.read()
        file.close()
        return data

    def update_file(self, text_data):
        file = open(self.filename, 'w')
        file.write(text_data)
        file.close()