window.readFileAsArrayBuffer = async (file) => {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.onload = (event) => resolve(new Uint8Array(event.target.result));
        reader.onerror = (error) => reject(error);
        reader.readAsArrayBuffer(file);
    });
};

window.readExcelFile = async (arrayBuffer) => {
    try {
        const workbook = XLSX.read(arrayBuffer, { type: 'array' });
        const sheetName = workbook.SheetNames[0];
        const worksheet = workbook.Sheets[sheetName];
        const data = XLSX.utils.sheet_to_json(worksheet, { header: 1 });

        const staffList = [];
        for (let i = 1; i < data.length; i++) {
            const row = data[i];
            const staff = {
                StaffCode: row[0]?.toString().trim() || "",
                Name: row[1]?.toString().trim() || "",
                AccountFpt: row[2]?.toString().trim() || "",
                AccountFe: row[3]?.toString().trim() || "",
                Status: parseInt(row[4]) || 1
            };

            const locationCode = row[5]?.toString().trim() || "";
            if (locationCode) {
                const parts = locationCode.split('-');
                if (parts.length === 3) {
                    staff.LocationCode = parts[0];
                    staff.DepartmentCode = parts[1];
                    staff.MajorCode = parts[2];
                } else {
                    staff.LocationCode = "";
                    staff.DepartmentCode = "";
                    staff.MajorCode = "";
                }
            } else {
                staff.LocationCode = "";
                staff.DepartmentCode = "";
                staff.MajorCode = "";
            }

            if (staff.StaffCode && staff.Name) {
                staffList.push(staff);
            }
        }

        return staffList;
    } catch (error) {
        console.error("Error reading Excel file:", error);
        throw new Error("Không thể đọc file Excel: " + error.message);
    }
};