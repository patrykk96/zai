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


kalkulator = Calculator(20, 10)
print(kalkulator.add())
print(kalkulator.difference())
print(kalkulator.multiply())
print(kalkulator.devide())
