import moment from "moment";

export function formatDate(date) {
    return moment(date).format("DD-MM-YYYY, HH:mm:ss");
}

export function formatDateToMonthAndYear(date) {
    return moment(date).format("MMMM YYYY");
}