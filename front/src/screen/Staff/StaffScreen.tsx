import { Box, Paper, Table, TableBody, TableCell, TableHead, TableRow, TableSortLabel, Typography, useTheme } from "@mui/material";
import { useQuery } from "@tanstack/react-query";
import { IStaff } from "../../api/staff/type";
import { apis } from "../../api";
import { formatDate } from "../../utils/datetime";

interface HeadCell {
    id: string;
    label: string;
}

const headCells: readonly HeadCell[] = [
    { id: 'fullName', label: 'Name' },
    { id: 'birthday', label: 'Birthday' },
    { id: 'gender', label: "Gender" },
    { id: 'staffId', label: 'Staff ID' },
    { id: 'action', label: '' }
];


function EnhancedTableHead() {
    return (
        <TableHead>
            <TableRow>
                {headCells.map((headCell) => (
                    <TableCell
                        key={headCell.id}
                        align='left'
                        padding='normal'
                    >
                        <TableSortLabel
                        >
                            {headCell.label}
                        </TableSortLabel>
                    </TableCell>
                ))}
            </TableRow>
        </TableHead>
    );
}


const StaffScreen = () => {


    const { data } = useQuery<IStaff[]>({
        queryKey: ['staffs'], queryFn: apis.staff.getStaff
    })
    console.log(data)

    const theme = useTheme();

    const borderColor = theme.palette.divider;
    return (
        <Box>

            <Paper variant="outlined" sx={{ mx: 2, mt: 1, border: `1px solid ${borderColor}` }}>
                <Table
                    sx={{ minWidth: 750 }}
                    aria-labelledby="tableTitle"
                    size='medium'
                >
                    <EnhancedTableHead />
                    <TableBody>
                        {data?.map((staff: IStaff) => {
                            return (
                                <TableRow key={staff.id}>
                                    <TableCell>
                                        <Typography color="textSecondary" variant="subtitle2">
                                            {staff.fullName}
                                        </Typography>
                                    </TableCell>
                                    <TableCell>
                                        <Typography color="textSecondary" variant="subtitle2">
                                            {formatDate(staff.birthday)}
                                        </Typography>
                                    </TableCell>
                                    <TableCell>
                                        <Typography color="textSecondary" variant="subtitle2">
                                            {staff.gender}
                                        </Typography>
                                    </TableCell>
                                    <TableCell>
                                        <Typography color="textSecondary" variant="subtitle2">
                                            {staff.staffId}
                                        </Typography>
                                    </TableCell>
                                </TableRow>
                            )
                        })}
                    </TableBody>
                </Table>
            </Paper>
        </Box>
    )
}

export default StaffScreen;