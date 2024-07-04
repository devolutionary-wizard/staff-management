import { API_ENDPOINTS } from "../../config/apiEndpoints"
import { API } from "../base"
import { ICreateStaff, IStaff, IUpdateStaff } from "./type"

export const STAFF_API = {
    async getStaff(): Promise<IStaff[]> {
        try {
            const res = await API.get(API_ENDPOINTS.STAFFS.GET_ALL())
            return res.data.data
        } catch (error) {
            console.error("Failed to get staff:", error)
            throw error
        }
    },
    async createStaff(params: ICreateStaff) {
        try {
            const res = await API.post(API_ENDPOINTS.STAFFS.CREATE(), params)
            console.log(res)
            return res
        } catch (error) {
            console.error("Failed to create staff:", error)
            throw error
        }
    },
    async deleteStaff(id: string) {
        try {
            const res = await API.delete(API_ENDPOINTS.STAFFS.DELETE(id))
            return res
        } catch (error) {
            console.error("Failed to delete staff:", error)
            throw error
        }
    },
    async updateStaff(params: IUpdateStaff) {
        try {
            const res = await API.put(API_ENDPOINTS.STAFFS.UPDATE(params.id.toString()), params)
            return res
        } catch (error) {
            console.error("Failed to update staff:", error)
            throw error
        }
    },
}