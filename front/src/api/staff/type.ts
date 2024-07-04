export interface IStaff {
    id: number,
    staffId: string,
    fullName: string,
    birthday: string,
    gender: string
}

export interface ICreateStaff {
    staffId: string,
    fullName: string,
    birthday: string,
    gender: string
}

export interface IUpdateStaff {
    id: number,
    staffId?: string,
    fullName?: string,
    birthday?: string,
    gender?: string
}
