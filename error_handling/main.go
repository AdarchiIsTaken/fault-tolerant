package main

import (
	"fmt"
	"io"
	"os"
)

func main() {
	fmt.Println("Ejemplo de retorno explicito")
	contents, err := readFile("archivo.txt")
	if err == nil {
		fmt.Println("El archivo contiene: ")
		fmt.Println(contents)
	}

	fmt.Println("El programa finaliza sin crashear")
}

func readFile(fileName string) (string, error) {
	file, err := os.Open(fileName)
	if err != nil {
		fmt.Println(err)
		return "", err
	}

	result, err := io.ReadAll(file)
	if err != nil {
		fmt.Println(err)
		return "", err
	}

	return string(result), nil
}