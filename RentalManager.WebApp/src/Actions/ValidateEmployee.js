export default function ValidateEmployee(employee) {
    let result = []
    if(employee.name.trim().length === 0){
        result.push("noName")
    }
    if(employee.surname.trim().length === 0){
        result.push("noSurname")
    }
    return(result)
}