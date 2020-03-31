class Calculator:
    def __init__(self, liczba_1, liczba_2):
        self.liczba1 = liczba_1
        self.liczba2 = liczba_2

    def add(self):
        return self.liczba1 + self.liczba2

    def difference(self):
        return self.liczba1 - self.liczba2

    def multiply(self):
        return self.liczba1 * self.liczba2

    def devide(self):
        return self.liczba1 / self.liczba2

class ScienceCalculator(Calculator):
    def potegowanie(self):
        return self.liczba1 ^ self.liczba2

    def reszta_z_dzielenia(self):
        return self.liczba1 % self.liczba2


kalkulator = ScienceCalculator(5, 3)
print(kalkulator.add())
print(kalkulator.difference())
print(kalkulator.multiply())
print(kalkulator.devide())
print(kalkulator.potegowanie())
print(kalkulator.reszta_z_dzielenia())
